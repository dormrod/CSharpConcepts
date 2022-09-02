using System;
using System.Diagnostics;
using CSharpConcepts.TypeComparison;
using NUnit.Framework;

namespace CSharpConcepts.Types;

public class TypeTests
{
    [Test]
    public void ValueTypes_HaveMinAndMaxRange_DeterminedByNumberOfBytes()
    {
        AssertSizeUnsigned(byte.MinValue, byte.MaxValue, sizeof(byte));
        AssertSizeSigned(short.MinValue, short.MaxValue, sizeof(short));
        AssertSizeSigned(int.MinValue, int.MaxValue, sizeof(int));
        AssertSizeSigned(long.MinValue, long.MaxValue, sizeof(long));

        void AssertSizeUnsigned<T>(T min, T max, int size)
        {
            Console.WriteLine($"Size of {typeof(T)} is {size} bytes. Range of values is {min}->{max}.");
            Assert.That(min, Is.EqualTo(0));
            Assert.That(max, Is.EqualTo((long) Math.Pow(2, size * 8) - 1));
        }
        
        void AssertSizeSigned<T>(T min, T max, int size)
        {
            var magnitude = (long) Math.Pow(2, size * 8 - 1);
            Console.WriteLine($"Size of {typeof(T)} is {size} bytes. Range of values is {min}->{max}.");
            Assert.That(min, Is.EqualTo(-magnitude));
            Assert.That(max, Is.EqualTo(magnitude - 1));
        }
    }
    
    [Test]
    public void ValueTypes_HaveDefaultValues()
    {
        Assert.That(default(bool), Is.EqualTo(false));
        Assert.That(default(byte), Is.EqualTo(0));
        Assert.That(default(char), Is.EqualTo(0));
        Assert.That(default(int), Is.EqualTo(0));
        Assert.That(default(decimal), Is.EqualTo(0));
    }
        
    [Test]
    public void NumericTypes_AreLessPerformant_WithIncreasedSize()
    {
        const int iterations = 1000000000;
        var swFloat = new Stopwatch();
        swFloat.Start();
        for (int i = 0; i < iterations; ++i)
        {
            float oneFloat = 1;
        }
        swFloat.Stop();
        
        var swDecimal = new Stopwatch();
        swDecimal.Start();
        for (int i = 0; i < iterations; ++i)
        {
            decimal oneDecimal = 1m;
        }
        swDecimal.Stop();
        
        Console.WriteLine($"Float took: {swFloat.ElapsedMilliseconds}ms");
        Console.WriteLine($"Decimal took: {swDecimal.ElapsedMilliseconds}ms");
        
        Assert.That(swFloat.ElapsedMilliseconds, Is.LessThan(swDecimal.ElapsedMilliseconds));
    }
    
    [Test]
    public void PassValueTypeByValue_ToMethod_DoesNotAlterOriginalVariable()
    {
        var x = 1;
        var y = PassValueTypeByValueAndIncrement(x);
        
        Assert.That(x, Is.EqualTo(1));
        Assert.That(y, Is.EqualTo(2));

        int PassValueTypeByValueAndIncrement(int v)
        {
            return ++v;
        }
    }
    
    [Test]
    public void PassReferenceTypeByValue_ToMethod_DoesNotAlterOriginalReference_ButCanAlterReferencedObject()
    {
        var x = new ReferenceType {IntValue = 1, StrValue = "a"};
        var y = PassReferenceTypeByValue(x);
        
        Assert.That(x.IntValue, Is.EqualTo(2));
        Assert.That(y, Is.Null);

        ReferenceType PassReferenceTypeByValue(ReferenceType r)
        {
            r.IntValue = 2;
            r = null;
            return r;
        }
    }
    
    [Test]
    public void PassStringTypeByValue_ToMethod_DoesNotAlterOriginalVariable()
    {
        var x = "a";
        var y = PassStringTypeByValueAndAppendX(x);
        
        Assert.That(x, Is.EqualTo("a"));
        Assert.That(y, Is.EqualTo("aX"));

        string PassStringTypeByValueAndAppendX(string s)
        {
            return $"{s}X";
        }
    }
    
    [Test]
    public void PassValueTypeByReference_ToMethod_AltersOriginalVariable()
    {
        var x = 1;
        var y = PassValueTypeByReferenceAndIncrement(ref x);
        
        Assert.That(x, Is.EqualTo(2));
        Assert.That(y, Is.EqualTo(2));

        int PassValueTypeByReferenceAndIncrement(ref int v)
        {
            return ++v;
        }
    }
    
    [Test]
    public void PassReferenceTypeByReference_ToMethod_AltersOriginalVariable()
    {
        var x = new ReferenceType {IntValue = 1, StrValue = "a"};
        var y = PassReferenceTypeByValueAndAppendX(ref x);
        
        Assert.That(x, Is.Null);
        Assert.That(y, Is.Null);

        ReferenceType PassReferenceTypeByValueAndAppendX(ref ReferenceType r)
        {
            r.IntValue = 2;
            r = null;
            return r;
        }
    }
    
    [Test]
    public void PassStringTypeByReference_ToMethod_DoesNotAlterOriginalVariable()
    {
        var x = "a";
        var y = PassStringTypeByValueAndAppendX(ref x);
        
        Assert.That(x, Is.EqualTo("a"));
        Assert.That(y, Is.EqualTo("aX"));

        string PassStringTypeByValueAndAppendX(ref string s)
        {
            return $"{s}X";
        }
    }
    
    [Test]
    public void BoxingAndUnboxing_ValueType()
    {
        int x = 1;
        object y = x;
        y = 2;
        int z = (int) y;

        Assert.That(x, Is.EqualTo(1));
        Assert.That(y, Is.EqualTo(2));
        Assert.That(z, Is.EqualTo(2));

        z = 3;
        Assert.That(x, Is.EqualTo(1));
        Assert.That(y, Is.EqualTo(2));
        Assert.That(z, Is.EqualTo(3));
    }
}