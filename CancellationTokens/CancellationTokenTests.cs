using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace CSharpConcepts.CancellationTokens;

public class CancellationTokenTests
{
    [Test]
    public void Task_IsCancelled_AfterSpecifiedPeriod()
    {
        var cts = new CancellationTokenSource();
        cts.CancelAfter(200);

        Assert.ThrowsAsync<TaskCanceledException>(async () => await LongRunningTask(cts.Token));
        
        async Task LongRunningTask(CancellationToken ct)
        {
            await Task.Delay(250);

            if (ct.IsCancellationRequested)
                throw new TaskCanceledException("Out of time!");

            await Task.Delay(250);
        }
    }
    
    [Test]
    public void Task_IsCancelled_AfterCancellationSignalled()
    {
        var cts = new CancellationTokenSource();

        Task.Run(CancelAfter100ms);
        
        Assert.ThrowsAsync<TaskCanceledException>(async () => await LongRunningTask(cts.Token));
        
        async Task LongRunningTask(CancellationToken ct)
        {
            await Task.Delay(250);
            
            if (ct.IsCancellationRequested)
                throw new TaskCanceledException("Out of time!");

            await Task.Delay(250);
        }

        async Task CancelAfter100ms()
        {
            Thread.Sleep(100);
            cts.Cancel();
        }
    }
}