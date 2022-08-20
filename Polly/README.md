# Polly 

See Microsoft docs [here](https://docs.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/implement-http-call-retries-exponential-backoff-polly).
See Polly repo [here](https://github.com/App-vNext/Polly).

## Concept

Polly is a "resilience and transient-fault-handling library".
Essentially it provides functionality for configuring retry logic when accessing external services which may experience temporary unavailability.

## Examples

The directory has a `FlakeyService` which succeeds (deterministically) only every once in every 5 times its `Get` method is called.

See `PollyTests` for a policy that retries with exponential backoff until a successful response is obtained.