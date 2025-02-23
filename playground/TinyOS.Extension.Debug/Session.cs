﻿using Microsoft.VisualStudio.Shared.VSCodeDebugProtocol;
using Microsoft.VisualStudio.Shared.VSCodeDebugProtocol.Messages;

namespace TinyOS.Extension.Debug;

public abstract class Session: DebugAdapterBase, IProcessLogger {

    protected Session(Stream input, Stream output) {
        //LogConfig.InitializeLog();
        base.InitializeProtocolClient(input, output);
    }

    protected abstract ICustomLogger GetLogger();
    protected abstract void OnUnhandledException(Exception ex);

    public void Start() {
        Protocol.LogMessage += LogMessage;
        Protocol.DispatcherError += LogError;
        Protocol.Run();
    }

    protected void SendConsoleEvent(OutputEvent.CategoryValue category, string message) {
        Protocol.SendEvent(new OutputEvent(message.Trim() + Environment.NewLine) {
            Category = category
        });
    }
    protected T DoSafe<T>(Func<T> handler) {
        try {
            return handler.Invoke();
        } catch (Exception ex) {
            LogException(ex);
            return default;
        }
    }
    protected void DoSafe(Action handler) {
        try {
            handler.Invoke();
        } catch (Exception ex) {
            LogException(ex);
        }
    }

    public void OnOutputDataReceived(string stdout) {
        SendConsoleEvent(OutputEvent.CategoryValue.Stdout, stdout);
    }
    public void OnErrorDataReceived(string stderr) {
        SendConsoleEvent(OutputEvent.CategoryValue.Stderr, stderr);
    }

    private void LogMessage(object sender, LogEventArgs args) {
        GetLogger().LogMessage(args.Message);
    }
    private void LogError(object sender, DispatcherErrorEventArgs args) {
        GetLogger().LogError($"[Fatal] {args.Exception.Message}", args.Exception);
        OnUnhandledException(args.Exception);
    }
    private void LogException(Exception ex) {
        if (ex is ProtocolException)
            throw ex;
        GetLogger().LogError($"[Handled] {ex.Message}", ex);
        //ServerExtensions.ThrowException(ex.Message);
    }
}