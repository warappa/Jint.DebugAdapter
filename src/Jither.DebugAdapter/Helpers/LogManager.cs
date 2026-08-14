namespace Jither.DebugAdapter.Helpers;

public static class LogManager
{
    public static LogLevel Level { get; set; } = LogLevel.Quiet;
    public static ILogProvider Provider { get; set; } = new NullLogProvider();

    public static Logger GetLogger()
    {
        return new Logger();
    }

    public static void Log(LogLevel level, LoggerStringHandler message)
    {
        if (level >= Level && Provider is not null)
        {
            Provider.Log(level, message.GetFormattedText());
        }
    }

    public static void Log(LogLevel level, string message)
    {
        if (level >= Level && Provider is not null)
        {
            Provider.Log(level, message);
        }
    }
}
