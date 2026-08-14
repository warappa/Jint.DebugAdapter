using Acornima;

namespace Jint.DebugAdapter;

public class AcornimaPositionComparer : IComparer<Position>
{
    public static readonly AcornimaPositionComparer Default = new();

    public int Compare(Position x, Position y)
    {
        if (x.Line != y.Line)
        {
            return x.Line - y.Line;
        }

        return x.Column - y.Column;
    }
}
