namespace CSharpConcepts.DependencyInjection;

/// <summary>
/// Simple dependency class that returns the same string on method call.
/// </summary>
public class Dependency : IDependency
{
    public string DoSomething()
        => $"{nameof(Dependency)}.DoSomething has been called.";
}