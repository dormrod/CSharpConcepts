using System;
using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework;

namespace CSharpConcepts.TypeComparison;

public class TypeComparisonTests
{
    [Test]
    public void ValueTypes_AreEqual_WithSameValues()
    {
        var v1 = new ValueType {IntValue = 1, StrValue = "a"};
        var v2 = new ValueType {IntValue = 1, StrValue = "a"};
        var v3 = new ValueType {IntValue = 1, StrValue = "b"};
        
        Assert.That(v1, Is.EqualTo(v2)); 
        Assert.That(v1, Is.Not.EqualTo(v3)); 
    }
    
    [Test]
    public void ReferenceTypes_AreNotEqual_WithSameValues()
    {
        var r1 = new ReferenceType {IntValue = 1, StrValue = "a"};
        var r2 = new ReferenceType {IntValue = 1, StrValue = "a"};
        
        Assert.That(r1, Is.Not.EqualTo(r2)); 
    }
    
    [Test]
    public void EquatableReferenceTypes_AreEqual_WithSameValues()
    {
        var r1 = new ReferenceTypeEquatable {IntValue = 1, StrValue = "a"};
        var r2 = new ReferenceTypeEquatable {IntValue = 1, StrValue = "a"};
        var r3 = new ReferenceTypeEquatable {IntValue = 1, StrValue = "b"};
        
        Assert.That(r1, Is.EqualTo(r2)); 
        Assert.That(r1, Is.Not.EqualTo(r3)); 
    }
    
    [TestCase(100)]
    [TestCase(1000)]
    [TestCase(10000)]
    public void HashMapLookup_IsFaster_WithGoodHashingFunction(int numObjects)
    {
        var dictGood = new Dictionary<ReferenceTypeGoodHashCode, int>();
        var dictBad = new Dictionary<ReferenceTypeBadHashCode, int>();

        for (int i = 0; i < numObjects; ++i)
        {
            dictGood.Add(new ReferenceTypeGoodHashCode {IntValue = i, StrValue = "a"}, i);
            dictBad.Add(new ReferenceTypeBadHashCode {IntValue = i, StrValue = "a"}, i);
        }

        var sw = new Stopwatch();
        sw.Start();
        for (int i = 0; i < 10_000; ++i)
        {
            _ = dictGood[new ReferenceTypeGoodHashCode {IntValue = 1, StrValue = "a"}];
        }
        sw.Stop();
        var tsGood = sw.ElapsedMilliseconds;
        sw.Reset();
        
        sw.Start();
        for (int i = 0; i < 10_000; ++i)
        {
            _ = dictBad[new ReferenceTypeBadHashCode {IntValue = 1, StrValue = "a"}];
        }
        sw.Stop();
        var tsBad = sw.ElapsedMilliseconds;
        
        Console.WriteLine($"Timing for 'good' hash: {tsGood}");
        Console.WriteLine($"Timing for 'bad' hash: {tsBad}");
        
        Assert.That(tsGood, Is.LessThan(tsBad));
    }
    
    [Test]
    public void TypeofComparison_OnlyTrueForSameDerivedType()
    {
        Assert.That(typeof(ReferenceType), Is.Not.EqualTo(typeof(ReferenceTypeEquatable)));
    }

    [Test]
    public void IsTypeComparison_TrueIfAnyDerivedType()
    {
        var parent = new ReferenceType {IntValue = 1, StrValue = "a"};
        var child = new ReferenceTypeEquatable {IntValue = 1, StrValue = "a"};
        
        Assert.That(parent is ReferenceType, Is.EqualTo(true));
        Assert.That(parent is ReferenceTypeEquatable, Is.EqualTo(false));
        Assert.That(child is ReferenceType, Is.EqualTo(true));
        Assert.That(child is ReferenceTypeEquatable, Is.EqualTo(true));
    }
}