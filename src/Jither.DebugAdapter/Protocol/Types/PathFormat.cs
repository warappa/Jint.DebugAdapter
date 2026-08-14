using Jither.DebugAdapter.Helpers;

namespace Jither.DebugAdapter.Protocol.Types;

/// <completionlist cref="PathFormat"/>
public class PathFormat : StringEnum<PathFormat>
{
    public static readonly PathFormat Path = Create("path");
    public static readonly PathFormat Uri = Create("uri");
}
