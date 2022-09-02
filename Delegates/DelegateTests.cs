using System;
using NUnit.Framework;

namespace CSharpConcepts.Delegates;

public class DelegateTests
{
    private delegate string ManipulateStringDelegate(string s);
    private delegate void ChangeCounter();
    
    [Test]
    public void SingleCastDelegate_SuccessfullyInvoked()
    {
        ManipulateStringDelegate manipulateString = ExampleFunctions.Reflect;
        
        Assert.That(manipulateString("abc"), Is.EqualTo("abccba"));
        Assert.That(manipulateString.Invoke("abc123"), Is.EqualTo("abc123321cba"));

        manipulateString = ExampleFunctions.TakeEveryOtherLetter;
        Assert.That(manipulateString("abc"), Is.EqualTo("ac"));
        Assert.That(manipulateString.Invoke("abc123"), Is.EqualTo("ac2"));
    }
    
    [Test]
    public void ConstructedSingleCastDelegate_SuccessfullyInvoked()
    {
        var manipulateString = new ManipulateStringDelegate(ExampleFunctions.TakeEveryOtherLetter);
        
        Assert.That(manipulateString("abc"), Is.EqualTo("ac"));
        Assert.That(manipulateString.Invoke("abc123"), Is.EqualTo("ac2"));
    }
    
    [Test]
    public void MultiCastDelegate_SuccessfulInvoked()
    {
        ChangeCounter changeCounter = ExampleFunctions.ResetCounter;
        changeCounter += ExampleFunctions.AddOneToCounter;
        changeCounter += ExampleFunctions.AddOneToCounter;
        changeCounter += ExampleFunctions.AddTenToCounter;
        
        changeCounter.Invoke();
        
        Assert.That(ExampleFunctions.Counter, Is.EqualTo(12));

        changeCounter -= ExampleFunctions.AddOneToCounter;
        changeCounter.Invoke();
        
        Assert.That(ExampleFunctions.Counter, Is.EqualTo(11));
    }
    
    [Test]
    public void SingleCastDelegate_WithAnonymousMethod_SuccessfulInvoked()
    {
        ManipulateStringDelegate manipulateString = delegate(string s)
        {
            return $"'{s}'";
        };

        Assert.That(manipulateString("abc"), Is.EqualTo("'abc'"));
    }
    
    [Test]
    public void SingleCastDelegate_WithLambdaFunction_SuccessfulInvoked()
    {
        ManipulateStringDelegate manipulateString = s => $"'{s}'";

        Assert.That(manipulateString("abc"), Is.EqualTo("'abc'"));
    }

    [Test]
    public void Action_SuccessfullyInvoked()
    {
        var actual = 0;
        var actionNoArgs = () => { ++actual; };
        var actionWithArgs = (int m, int n) => { actual += m * n; };
        var actionWithArgsExplicit = new Action<int, int>((m, n) => { actual *= m + n; });

        actionNoArgs();
        actionWithArgs(2, 3);
        actionWithArgsExplicit(1, 2);
        Assert.That(actual, Is.EqualTo(21));
    }
    
    [Test]
    public void Func_SuccessfullyInvoked()
    {
        var funcNoArgs = () => 1;
        var funcWithArgs = (int x, int m, int n) =>
        {
            x += m * n;
            return x;
        };
        var funcWithArgsExplicit = new Func<int, int, int, int>((x, m, n) => x * (m + n));

        var actual = funcWithArgsExplicit(funcWithArgs(funcNoArgs.Invoke(), 2, 3), 1, 2);
        Assert.That(actual, Is.EqualTo(21));
    }
    
    [Test]
    public void Predicate_SuccessfullyInvoked()
    {
        var predicateIsEven = new Predicate<int>(n => n % 2 == 0);

        Assert.That(predicateIsEven(2), Is.EqualTo(true));
        Assert.That(predicateIsEven(1), Is.EqualTo(false));
    }
}