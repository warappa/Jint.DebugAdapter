namespace Jither.DebugAdapter.Helpers;

public interface ILogProvider
{
    void Log(LogLevel level, string message);
}
