namespace CSharpConcepts.DependencyInjection;

/// <summary>
/// Class dependent on IDependency, injected via the constructor. 
/// </summary>
public class Dependent : IDependent
{
    private readonly IDependency _dependency;

    public Dependent(IDependency dependency)
    {
        _dependency = dependency;
    }

    public string DoSomethingUsingDependency()
        => _dependency.DoSomething();
}