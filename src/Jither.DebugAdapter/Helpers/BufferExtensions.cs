using System.Buffers;
using System.Runtime.CompilerServices;

namespace Jither.DebugAdapter.Helpers;

public static class BufferExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ReadOnlySpan<byte> ToSpan(in this ReadOnlySequence<byte> buffer)
    {
        if (buffer.IsSingleSegment)
        {
            return buffer.FirstSpan;
        }

        return buffer.ToArray();
    }
}
