using System;
using NUnit.Framework;
using SimpleInjector;

namespace CSharpConcepts.DependencyInjection;

public class DependencyInjectionTests
{
    [Test]
    public void DependencyBehaviour_CannotBeControlled_WithoutDI()
    {
        var sut = new DependentNoDi();
        Assert.DoesNotThrow(() => Console.WriteLine(sut.DoSomethingUsingDependency()));
    }

    [Test]
    public void DependencyAndDependent_CanBeRegisteredAndCalled()
    {
        var container = new Container();
        container.Register<IDependency, Dependency>(Lifestyle.Singleton);
        container.Register<IDependent, Dependent>(Lifestyle.Singleton);

        var sut = container.GetInstance<IDependent>();
        Assert.DoesNotThrow(() => Console.WriteLine(sut.DoSomethingUsingDependency()));
    }
    
    [Test]
    public void SameInstancesUsed_WhenDependentAndDependencyRegisteredAsSingleton()
    {
        var container = new Container();
        container.Register<IDependency, DependencyWithCounter>(Lifestyle.Singleton);
        container.Register<IDependent, Dependent>(Lifestyle.Singleton);

        var sut = container.GetInstance<IDependent>();
        StringAssert.Contains("1 time", sut.DoSomethingUsingDependency());
        
        sut = container.GetInstance<IDependent>();
        StringAssert.Contains("2 time(s)", sut.DoSomethingUsingDependency(), "Counter should be incremented on same instance");
    }
    
    [Test]
    public void SameDependencyInstanceUsed_WhenDependencyRegisteredAsSingleton()
    {
        var container = new Container();
        container.Register<IDependency, DependencyWithCounter>(Lifestyle.Singleton);
        container.Register<IDependent, Dependent>(Lifestyle.Transient);

        var sut = container.GetInstance<IDependent>();
        StringAssert.Contains("1 time", sut.DoSomethingUsingDependency());

        sut = container.GetInstance<IDependent>();
        StringAssert.Contains("2 time(s)", sut.DoSomethingUsingDependency(), "Counter should be incremented on same instance");
    }
    
    [Test]
    public void DifferentInstancesUsed_WhenDependentAndDependencyRegisteredAsTransient()
    {
        var container = new Container();
        container.Register<IDependency, DependencyWithCounter>(Lifestyle.Transient);
        container.Register<IDependent, Dependent>(Lifestyle.Transient);

        var sut = container.GetInstance<IDependent>();
        StringAssert.Contains("1 time", sut.DoSomethingUsingDependency());

        sut = container.GetInstance<IDependent>();
        StringAssert.Contains("1 time", sut.DoSomethingUsingDependency(), "Counter should not be incremented on different instance");
    }
}