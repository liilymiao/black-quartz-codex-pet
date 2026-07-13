# Black Quartz Companion for Windows

[中文说明](README.zh-CN.md)

This folder builds a standalone Windows desktop companion from the existing Black Quartz v2 atlas. It does not require Codex or a separate .NET installation.

## Controls

- Drag with the left mouse button to move Black Quartz.
- Click without dragging to switch between processing and review.
- Right-click to select an animation, toggle pointer following or always-on-top, change scale, or exit.
- When idle, the archive aperture follows the pointer using the atlas's sixteen look directions.

## Build

Install the .NET 8 SDK, then run from `windows/BlackQuartzCompanion/`:

```powershell
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true
```

The executable is emitted under:

```text
bin/Release/net8.0-windows/win-x64/publish/BlackQuartzCompanion.exe
```

The released binary is unsigned. Windows SmartScreen may therefore show an unknown-publisher warning. Build from source if you prefer not to run an unsigned binary.

The original character and setting rights remain with their respective owners. See the repository's `RIGHTS-NOTICE.md`.
