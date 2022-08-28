using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace CSharpConcepts.FinalisersAndDisposal;

public class FinalisersAndDisposalTests
{
    [Test]
    public void ObjectWithFinaliser_IsGarbageCollected_WhenNoReferences()
    {
        var refs = new List<WeakReference>();

        var objs = new List<ChildWithFinaliser>();

        refs.Add(CreateWeakReference(() =>
        {
            var child = new ChildWithFinaliser();
            objs.Add(child);
            return new WeakReference(child);
        }));

        objs.Clear();
        GC.Collect();
    
        Assert.That(refs.Single().IsAlive, Is.EqualTo(false));
        
        WeakReference CreateWeakReference<T>(Func<T> factory)
            => new WeakReference(factory());
    }

    [Test]
    public async Task DisposingFlags_AreSet_WhenObjectExplicitlyDisposed()
    {
        var obj = new ExampleWithDispose();
        
        Assert.That(obj.IsDisposing, Is.EqualTo(false));
        Assert.That(obj.IsDisposed, Is.EqualTo(false));
        
        var disposeTask = Task.Run(() => obj.Dispose());
        Thread.Sleep(50);
        Assert.That(obj.IsDisposing, Is.EqualTo(true));
        Assert.That(obj.IsDisposed, Is.EqualTo(false));
        
        await disposeTask;

        Assert.That(obj.IsDisposing, Is.EqualTo(true));
        Assert.That(obj.IsDisposed, Is.EqualTo(true));
    }
    
    [Test]
    public void UsingKeyword_CallsDispose_WhenObjectOutOfScope()
    {
        {
            using var obj = new ExampleWithDispose();
            Assert.Throws<IOException>(() => new ExampleWithDispose());
        }

        Assert.DoesNotThrow(() => new ExampleWithDispose());
    }
}