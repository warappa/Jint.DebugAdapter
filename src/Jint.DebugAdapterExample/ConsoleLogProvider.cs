using Jither.DebugAdapter.Helpers;

namespace Jint.DebugAdapterExample;

public class ConsoleLogProvider : ILogProvider
{
    private readonly ConsoleColor defaultColor;

    public ConsoleLogProvider()
    {
        defaultColor = Console.ForegroundColor;
    }

    public void Log(LogLevel level, string message)
    {
        Console.ForegroundColor = level switch
        {
            LogLevel.Verbose => ConsoleColor.Gray,
            LogLevel.Info => ConsoleColor.White,
            LogLevel.Warning => ConsoleColor.Yellow,
            LogLevel.Error => ConsoleColor.Red,
            _ => ConsoleColor.White
        };

        Console.WriteLine(message);
        Console.ForegroundColor = defaultColor;
    }
}

public class FileLogProvider : ILogProvider
{
    private readonly FileStream stream;
    private readonly StreamWriter writer;

    public FileLogProvider(string path)
    {
        stream = File.Create(path);
        writer = new StreamWriter(stream);
    }

    public void Log(LogLevel level, string message)
    {
        writer.WriteLine($"{level}: {message}");
        writer.Flush();
    }
}
