using System;

namespace CSharpConcepts.Polly;

/// <summary>
/// Service which succeeds deterministically only every 1 in 5 times Get is called.
/// </summary>
public class FlakyService
{
    private int _counter;

    public int Get()
        => ++_counter % 5 == 0 
            ? 200 
            : throw new InvalidOperationException("Something went wrong!");
}