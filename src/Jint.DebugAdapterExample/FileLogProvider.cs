using Jither.DebugAdapter.Helpers;

namespace Jint.DebugAdapterExample;

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
