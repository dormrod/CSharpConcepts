using System;
using NUnit.Framework;

namespace CSharpConcepts.TypeConversion;

public class TypeCastingTests
{
    [Test]
    public void IntType_CanBeImplicitlyCast_ToDoubleType()
    {
        int value1 = 1;
        double value2 = 123;
        var value3 = value1 + value2;
        
        Assert.That(value1.GetType(), Is.EqualTo(typeof(int)));
        Assert.That(value2.GetType(), Is.EqualTo(typeof(double)));
        Assert.That(value3.GetType(), Is.EqualTo(typeof(double)));
    }
    
    [Test]
    public void DoubleType_CanBeExplicitlyCast_ToIntType()
    {
        int value1 = 1;
        double value2 = 123;
        var value3 = value1 + (int) value2;
        
        Assert.That(value1.GetType(), Is.EqualTo(typeof(int)));
        Assert.That(value2.GetType(), Is.EqualTo(typeof(double)));
        Assert.That(value3.GetType(), Is.EqualTo(typeof(int)));
    }

    [Test]
    public void ChildType_CanBeImplicitlyOrExplicitlyCast_ToParentType()
    {
        var child = new ChildType {IntValue = 2, StrValue = "child"};
        var parent = new ParentType {IntValue = 3};
        var implicitCast = parent + child;
        var explicitCast = (ParentType) child;
        
        Assert.That(implicitCast.IntValue, Is.EqualTo(5));
        Assert.That(explicitCast.IntValue, Is.EqualTo(2));
    }
    
    [Test]
    public void ParentType_CannotBeCast_ToChildType()
    {
        var parent = new ParentType {IntValue = 3};

        Assert.Throws<InvalidCastException>(() => _ = (ChildType) parent);
    }
    
    [Test]
    public void ParentType_CanBeCast_ToUserDefinedTypes()
    {
        var parent = new ParentType {IntValue = 3};
        int implicitCast = parent;
        string explicitCast = (string) parent;
        var methodConversion = parent.ToChildType();

        Assert.That(implicitCast, Is.EqualTo(3));
        Assert.That(explicitCast, Is.EqualTo("'3'"));
        Assert.That(methodConversion.IntValue, Is.EqualTo(3));
        Assert.That(methodConversion.StrValue, Is.EqualTo("'3'"));
    }

    [Test]
    public void CanConvertBetweenTypes_UsingHelperMethods()
    {
        var trueStr = "true";
        
        Assert.That(Convert.ToBoolean(trueStr), Is.EqualTo(true));
        Assert.Throws<FormatException>(() => _ = Convert.ToBoolean("foo"));
        
        Assert.That(bool.Parse(trueStr), Is.EqualTo(true));
        Assert.Throws<FormatException>(() => _ = bool.Parse("foo"));
        
        var parseHappy = bool.TryParse(trueStr, out var trueStrParsed);
        var parseUnhappy = bool.TryParse("foo", out var fooStrParsed);
        Assert.That(parseHappy, Is.EqualTo(true));
        Assert.That(trueStrParsed, Is.EqualTo(true));
        Assert.That(parseUnhappy, Is.EqualTo(false));
        Assert.That(fooStrParsed, Is.EqualTo(false));
    }
}