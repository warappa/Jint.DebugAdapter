namespace Jither.DebugAdapter.Helpers;

public class NullLogProvider : ILogProvider
{
    public void Log(LogLevel level, string message)
    {
    }
}
