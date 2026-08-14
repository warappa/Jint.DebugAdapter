namespace Jint.DebugAdapterExample;

public class AppOptions
{
    public string DemoId { get; } = "files";
    public string? Endpoint { get; }

    public AppOptions(string[] args, Dictionary<string, Action<AppOptions>> demosById)
    {
        foreach (var arg in args)
        {
            if (demosById.ContainsKey(arg))
            {
                DemoId = arg;
                continue;
            }
            Endpoint = arg;
        }
    }
}
