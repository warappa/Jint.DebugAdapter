using System.Runtime.CompilerServices;
using System.Text;

namespace Jither.DebugAdapter.Helpers;

[InterpolatedStringHandler]
public ref struct LoggerStringHandler
{
    private readonly StringBuilder builder;

    // Suppress "Remove unused parameter" - formattedCount, although unused, is required by compiler for
    // InterpolatedStringHandler.
#pragma warning disable IDE0060

    public LoggerStringHandler(int literalLength, int formattedCount, LogLevel level, out bool shouldAppend)
    {
        shouldAppend = level >= LogManager.Level;
        if (!shouldAppend)
        {
            builder = null;
            return;
        }
        builder = new StringBuilder(literalLength);
    }

    public LoggerStringHandler(int literalLength, int formattedCount)
    {
        builder = new StringBuilder(literalLength);
    }

#pragma warning restore IDE0060 // Remove unused parameter

    public void AppendLiteral(string str)
    {
        builder.Append(str);
    }

    public void AppendFormatted<T>(T value)
    {
        builder.Append(value?.ToString());
    }

    internal string GetFormattedText() => builder.ToString();
}
