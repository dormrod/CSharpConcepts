namespace CSharpConcepts.DependencyInjection;

/// <summary>
/// Class dependent on IDependency, with is hard-coded. 
/// </summary>
public class DependentNoDi : IDependent
{
    private readonly IDependency _dependency;

    public DependentNoDi()
    {
        _dependency = new Dependency();
    }

    public string DoSomethingUsingDependency()
        => _dependency.DoSomething();
}