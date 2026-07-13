using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace BlackQuartzCompanion;

public partial class MainWindow : Window
{
    private const int CellWidth = 192;
    private const int CellHeight = 208;
    private const double PointerDeadZoneDip = 24;

    private static readonly IReadOnlyDictionary<CompanionState, AnimationDefinition> Animations =
        new Dictionary<CompanionState, AnimationDefinition>
        {
            [CompanionState.Idle] = new(0, [280, 110, 110, 140, 140, 320], true),
            [CompanionState.Acknowledge] = new(3, [140, 140, 140, 280], false),
            [CompanionState.Jump] = new(4, [140, 140, 140, 140, 280], false),
            [CompanionState.Failed] = new(5, [140, 140, 140, 140, 140, 140, 140, 240], false),
            [CompanionState.Waiting] = new(6, [150, 150, 150, 150, 150, 260], true),
            [CompanionState.Processing] = new(7, [120, 120, 120, 120, 120, 220], true),
            [CompanionState.Review] = new(8, [150, 150, 150, 150, 150, 280], false),
            [CompanionState.LookAround] = new(9, Enumerable.Repeat(135, 16).ToArray(), false)
        };

    private readonly DispatcherTimer _timer = new() { Interval = TimeSpan.FromMilliseconds(25) };
    private readonly Stopwatch _frameClock = Stopwatch.StartNew();
    private BitmapSource? _atlas;
    private CompanionState _state = CompanionState.Idle;
    private int _frameIndex;
    private bool _followPointer = true;
    private bool _showingPointerDirection;

    public MainWindow()
    {
        InitializeComponent();
        Loaded += OnLoaded;
        _timer.Tick += OnAnimationTick;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        _atlas = LoadEmbeddedAtlas();
        SetScale(1.0);
        PlaceNearBottomRight();
        SetState(CompanionState.Idle);
        _timer.Start();
    }

    private static BitmapSource LoadEmbeddedAtlas()
    {
        var uri = new Uri("pack://application:,,,/Assets/spritesheet.png", UriKind.Absolute);
        var image = new BitmapImage();
        image.BeginInit();
        image.UriSource = uri;
        image.CacheOption = BitmapCacheOption.OnLoad;
        image.EndInit();
        image.Freeze();
        return image;
    }

    private void OnAnimationTick(object? sender, EventArgs e)
    {
        if (_atlas is null)
        {
            return;
        }

        if (_state == CompanionState.Idle && _followPointer && TryShowPointerDirection())
        {
            _showingPointerDirection = true;
            return;
        }

        if (_showingPointerDirection)
        {
            _showingPointerDirection = false;
            _frameIndex = 0;
            _frameClock.Restart();
            ShowAnimationFrame(Animations[_state], 0);
            return;
        }

        var animation = Animations[_state];
        if (_frameClock.ElapsedMilliseconds < animation.Durations[_frameIndex])
        {
            return;
        }

        _frameClock.Restart();
        _frameIndex++;

        if (_frameIndex >= animation.Durations.Length)
        {
            if (animation.Loop)
            {
                _frameIndex = 0;
            }
            else
            {
                SetState(CompanionState.Idle);
                return;
            }
        }

        ShowAnimationFrame(animation, _frameIndex);
    }

    private bool TryShowPointerDirection()
    {
        if (!TryGetCursorPosition(out var cursor))
        {
            return false;
        }

        // GetCursorPos and PointToScreen both use physical screen pixels. This keeps
        // tracking accurate outside the WPF window, across DPI scales and monitors.
        var center = PointToScreen(new Point(ActualWidth / 2, ActualHeight / 2));
        var dx = cursor.X - center.X;
        var dy = cursor.Y - center.Y;
        var distance = Math.Sqrt(dx * dx + dy * dy);
        var dpi = VisualTreeHelper.GetDpi(this);
        var deadZone = PointerDeadZoneDip * Math.Max(dpi.DpiScaleX, dpi.DpiScaleY);
        if (distance < deadZone)
        {
            return false;
        }

        var degrees = Math.Atan2(dx, -dy) * 180 / Math.PI;
        if (degrees < 0)
        {
            degrees += 360;
        }

        var directionIndex = (int)Math.Round(degrees / 22.5) % 16;
        ShowDirectionFrame(directionIndex);
        return true;
    }

    private static bool TryGetCursorPosition(out Point cursor)
    {
        if (GetCursorPos(out var nativePoint))
        {
            cursor = new Point(nativePoint.X, nativePoint.Y);
            return true;
        }

        cursor = default;
        return false;
    }

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool GetCursorPos(out NativePoint point);

    [StructLayout(LayoutKind.Sequential)]
    private struct NativePoint
    {
        public int X;
        public int Y;
    }

    private void ShowAnimationFrame(AnimationDefinition animation, int index)
    {
        if (_atlas is null)
        {
            return;
        }

        if (_state == CompanionState.LookAround)
        {
            ShowDirectionFrame(index);
            return;
        }

        SpriteImage.Source = new CroppedBitmap(
            _atlas,
            new Int32Rect(index * CellWidth, animation.Row * CellHeight, CellWidth, CellHeight));
    }

    private void ShowDirectionFrame(int directionIndex)
    {
        if (_atlas is null)
        {
            return;
        }

        var row = directionIndex < 8 ? 9 : 10;
        var column = directionIndex % 8;
        SpriteImage.Source = new CroppedBitmap(
            _atlas,
            new Int32Rect(column * CellWidth, row * CellHeight, CellWidth, CellHeight));
    }

    private void SetState(CompanionState state)
    {
        _state = state;
        _frameIndex = 0;
        _showingPointerDirection = false;
        _frameClock.Restart();
        ShowAnimationFrame(Animations[state], 0);
    }

    private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ButtonState != MouseButtonState.Pressed)
        {
            return;
        }

        var startLeft = Left;
        var startTop = Top;
        try
        {
            DragMove();
        }
        catch (InvalidOperationException)
        {
            return;
        }

        if (Math.Abs(Left - startLeft) < 3 && Math.Abs(Top - startTop) < 3)
        {
            SetState(_state == CompanionState.Processing ? CompanionState.Review : CompanionState.Processing);
        }
    }

    private void StateMenu_Click(object sender, RoutedEventArgs e)
    {
        if (sender is MenuItem { Tag: string tag } &&
            Enum.TryParse<CompanionState>(tag, out var state))
        {
            SetState(state);
        }
    }

    private void FollowPointerMenuItem_Click(object sender, RoutedEventArgs e)
    {
        _followPointer = FollowPointerMenuItem.IsChecked;
        if (!_followPointer)
        {
            SetState(CompanionState.Idle);
        }
    }

    private void TopmostMenuItem_Click(object sender, RoutedEventArgs e)
    {
        Topmost = TopmostMenuItem.IsChecked;
    }

    private void ScaleMenu_Click(object sender, RoutedEventArgs e)
    {
        if (sender is MenuItem { Tag: string tag } &&
            double.TryParse(tag, NumberStyles.Float, CultureInfo.InvariantCulture, out var scale))
        {
            SetScale(scale);
        }
    }

    private void SetScale(double scale)
    {
        Width = CellWidth * scale;
        Height = CellHeight * scale;
        SpriteImage.Width = Width;
        SpriteImage.Height = Height;
    }

    private void PlaceNearBottomRight()
    {
        var workArea = SystemParameters.WorkArea;
        Left = Math.Max(workArea.Left, workArea.Right - Width - 28);
        Top = Math.Max(workArea.Top, workArea.Bottom - Height - 28);
    }

    private void ExitMenu_Click(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }

    private sealed record AnimationDefinition(int Row, int[] Durations, bool Loop);

    private enum CompanionState
    {
        Idle,
        Acknowledge,
        Jump,
        Failed,
        Waiting,
        Processing,
        Review,
        LookAround
    }
}
