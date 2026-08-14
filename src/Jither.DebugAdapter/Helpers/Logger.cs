using System.Runtime.CompilerServices;

namespace Jither.DebugAdapter.Helpers;

// Temporary facade for logging with no dependencies.

public class Logger
{
    // Unfortunately, only this form will allow lazy evaluation of values in interpolated string
    public void Log(LogLevel level, [InterpolatedStringHandlerArgument("level")] LoggerStringHandler message)
    {
        LogManager.Log(level, message);
    }

    public void Info(LoggerStringHandler message)
    {
        LogManager.Log(LogLevel.Info, message);
    }

    public void Info(string message)
    {
        LogManager.Log(LogLevel.Info, message);
    }

    public void Verbose(LoggerStringHandler message)
    {
        LogManager.Log(LogLevel.Verbose, message);
    }

    public void Verbose(string message)
    {
        LogManager.Log(LogLevel.Verbose, message);
    }

    public void Warning(string message)
    {
        LogManager.Log(LogLevel.Warning, message);
    }

    public void Error(LoggerStringHandler message)
    {
        LogManager.Log(LogLevel.Error, message);
    }

    public void Error(string message)
    {
        LogManager.Log(LogLevel.Error, message);
    }
}
