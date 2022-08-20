using System;
using NUnit.Framework;
using Polly;

namespace CSharpConcepts.Polly;

public class PollyTests
{
    [Test]
    public void CanGetResponseFromFlakeyService_WithPollyWaitAndRetry()
    {
        var service = new FlakyService();

        var response = Policy
            .Handle<InvalidOperationException>(e => e.Message.Contains("went wrong"))
            .WaitAndRetry(
                10,
                attempt => 
                    TimeSpan.FromMilliseconds(125 * Math.Pow(2, Math.Min(4, attempt))), // 0.25s, 0.5s, 1s, 2s, 2s ...
                (ex, ts, at, _) =>
                    Console.WriteLine(
                        $"Failed to call service with exception: {ex.Message}. Retrying (attempt {at}) in {ts.TotalSeconds}s."))
            .Execute(() => service.Get());
        
        Assert.That(response, Is.EqualTo(200));
    }
}