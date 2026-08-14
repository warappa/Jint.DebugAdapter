using Jither.DebugAdapter.Protocol.Types;

namespace Jint.DebugAdapter.Variables;

public class ValueInfo
{
    public ValueInfo(string name)
    {
        Name = name;
    }

    public string Name { get; }
    public string Value { get; set; }
    public string Type { get; set; }
    public int VariablesReference { get; set; }
    public VariablePresentationHint PresentationHint { get; set; }
    public int? NamedVariables { get; set; }
    public int? IndexedVariables { get; set; }
    public string MemoryReference { get; set; }
    public string EvaluateName { get; set; }
}
