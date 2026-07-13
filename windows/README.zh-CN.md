# Black Quartz Windows 独立版

这个目录使用现有 Black Quartz v2 图集构建独立 Windows 桌面 Companion，不依赖 Codex，也不要求玩家另行安装 .NET。

## 操作

- 按住鼠标左键拖动 Black Quartz。
- 左键单击但不拖动，可在“处理中”和“审阅完成”之间切换。
- 右键可选择动画状态、开关指针跟随或始终置顶、调整大小并退出。
- 静止时，蓝色档案孔径会使用图集中的 16 个方向跟随全局鼠标；识别范围覆盖整个虚拟桌面和多显示器，中心仅保留一小圈静止死区。

## 构建

安装 .NET 8 SDK，然后在 `windows/BlackQuartzCompanion/` 中运行：

```powershell
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true
```

输出文件位于：

```text
bin/Release/net8.0-windows/win-x64/publish/BlackQuartzCompanion.exe
```

发布的 EXE 未进行代码签名，因此 Windows SmartScreen 可能显示“未知发布者”提示。如不希望运行未签名二进制文件，可以从本目录源码自行构建。

原角色与世界观相关权利归各自权利方所有，详见仓库根目录的 `RIGHTS-NOTICE.md`。
