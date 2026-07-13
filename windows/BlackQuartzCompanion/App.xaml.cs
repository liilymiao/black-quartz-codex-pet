using System.Threading;
using System.Windows;

namespace BlackQuartzCompanion;

public partial class App : Application
{
    private const string MutexName = "BlackQuartzCompanion.SingleInstance.2B9A51C4";
    private Mutex? _singleInstanceMutex;

    protected override void OnStartup(StartupEventArgs e)
    {
        var mutex = new Mutex(true, MutexName, out var isFirstInstance);
        if (!isFirstInstance)
        {
            mutex.Dispose();
            Shutdown();
            return;
        }

        _singleInstanceMutex = mutex;
        base.OnStartup(e);
        var window = new MainWindow();
        MainWindow = window;
        window.Show();
    }

    protected override void OnExit(ExitEventArgs e)
    {
        _singleInstanceMutex?.ReleaseMutex();
        _singleInstanceMutex?.Dispose();
        base.OnExit(e);
    }
}
