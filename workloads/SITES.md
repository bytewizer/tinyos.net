# SITES

## Source Code

[VSDebugAdapterHost](https://github.com/microsoft/VSDebugAdapterHost) 

[RaspberryDebugger](https://github.com/nforgeio/RaspberryDebugger)

[RemoteDebugLauncher](https://github.com/zedle/RemoteDebugLauncher)

[ZDCC_VS](https://github.com/ssjason123/SDCC_VS)

[VS_Win_Meadow_Extension](https://github.com/WildernessLabs/VS_Win_Meadow_Extension)

[LuaDebugger](https://github.com/Norbyte/bg3se/tree/main/LuaDebugger)

## MSbuild

[DeployTool](https://github.com/raffaeler/DeployTool)


## SSH
[Tmds.Ssh](https://github.com/tmds/Tmds.Ssh/tree/main)

[SshKeyGenerator](https://github.com/ShawInnes/SshKeyGenerator)

## Documentation

[Debug Adapter Protocol](https://microsoft.github.io/debug-adapter-protocol/)

[Offroad Debugging](https://github.com/Microsoft/MIEngine/wiki/Offroad-Debugging-of-.NET-Core-on-Linux---OSX-from-Visual-Studio)

### Troubleshooting
If you run into problems getting this to work, you can turn on logging by opening the VS Command Window (View->Other Windows->Command Window), and running: DebugAdapterHost.Logging /On /OutputWindow. This will then send logging messages to the output window ('Debug Adapter Host Log' pane). You can also add /Verbosity:debug for more information.


### Enable debugger logging

In the course of investigating a debugger issue, Microsoft might ask you to enable and collect debugger logs to help in diagnosis.

The following steps enable debugging in the current Visual Studio session:

1. Open a command window in Visual Studio by selecting **View** > **Other Windows** > **Command Window**.

1. Enter the following command:

   ```console
   DebugAdapterHost.Logging /On /OutputWindow
   ```

1. Start debugging and go through the steps necessary to reproduce your issue. During this time, debug logs appear in the **Output** window under **Debug Adapter Host Log**. You can then copy the logs from that window and paste into a GitHub issue, email, and so on.

   :::image type="content" source="media/debugger-logging-output.png" alt-text="Screenshot that shows Debugger logging output in the Output window in Visual Studio." lightbox="media/debugger-logging-output.png" border="false":::

1. If Visual Studio stops responding or you aren't otherwise able to access the **Output** window, restart Visual Studio, open a command window, and enter the following command:

   ```console
   DebugAdapterHost.Logging /On
   ```

1. Start debugging and reproduce your issue again. The debugger logs are located in `%temp%\DebugAdapterHostLog.txt`.