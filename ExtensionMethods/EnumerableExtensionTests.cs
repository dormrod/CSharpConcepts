using System.Linq;
using NUnit.Framework;

namespace CSharpConcepts.ExtensionMethods;

public class EnumerableExtensionTests
{
    [TestCase(5, true)]
    [TestCase(7, false)]
    [TestCase(10, false)]
    public void HasMoreThanNItems_AsExpected(int n, bool expected)
    {
        var sut = Enumerable.Range(1, 7);
        Assert.That(sut.HasMoreThanNItems(n), Is.EqualTo(expected));
    }
    
    [TestCase("test")]
    [TestCase(42)]
    public void YieldGivesEnumerable_ContainingOneItem(object item)
    {
        var actual = item.Yield().ToArray();
        Assert.That(actual, Has.Exactly(1).Items);
        Assert.That(actual.Single(), Is.EqualTo(item));
    }
}