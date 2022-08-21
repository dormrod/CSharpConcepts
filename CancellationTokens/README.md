# Cancellation Tokens

See Microsoft docs [here](https://docs.microsoft.com/en-us/dotnet/api/system.threading.cancellationtokensource?view=net-6.0).

## Concept

Cancellation tokens provide a mechanism to cancel long running tasks.
A `CancellationTokenSource` is created which has an associated `CancellationToken`.
This token can be passed into (long-running) tasks. 
The `CancellationTokenSource` can be signalled to cancel the token, after a set time period or for instance on an external trigger.
After cancellation is signaled, the task can check the `IsCancellationRequested` property on the token, and abort the task if necessary.

## Examples

See `CancellationTokenTests`.