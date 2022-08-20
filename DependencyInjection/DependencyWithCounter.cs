namespace CSharpConcepts.DependencyInjection;

/// <summary>
/// Dependency class that returns string with incremental counter on method call.
/// </summary>
public class DependencyWithCounter : IDependency
{
    private int _count = 1;

    public string DoSomething()
        => $"{nameof(Dependency)}.DoSomething has been called {_count++} time(s).";
}