using System;
using System.IO;
using System.Threading;

namespace CSharpConcepts.FinalisersAndDisposal;

public class ExampleWithDispose : IDisposable
{
    private const string TestFileName = "./test_disposal.txt";
    private readonly StreamWriter _streamWriter;
    private int _isDisposed;
    private int _isDisposing;

    public ExampleWithDispose()
    {
        // Can only have one open stream at any one time,
        // so will get exception if try to instantiate two instances
        // without dispose having been called
        _streamWriter = new StreamWriter(TestFileName);
    }
    
    public bool IsDisposed => Interlocked.CompareExchange(ref _isDisposed, 0, 0) == 1;
    public bool IsDisposing => Interlocked.CompareExchange(ref _isDisposing, 0, 0) == 1;
    
    public void Dispose()
    {
        if (Interlocked.CompareExchange(ref _isDisposing, 1, 0) == 1)
            return;
        
        Thread.Sleep(100);
        
        _streamWriter.Dispose();

        Interlocked.CompareExchange(ref _isDisposed, 1, 0);
    }
}