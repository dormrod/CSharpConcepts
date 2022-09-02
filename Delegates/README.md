# Delegates

## Concept

Delegates are type-safe function pointers i.e. they hold a reference to a method and call that method for execution.

Delegates can either hold a reference to a single method (single-cast) or multiple methods (multi-cast).
In the case of the multi-cast delegates, each method will be called in sequence.

An existing method does not have to be set to the delegate, instead an anonymous method may be used to define the method inline, and reduce verbosity.
This can be further achieved with a lambda function, which is even more shorthand for an anonymous method.

To even further simplify things, there are the generic delegates, which means you don't have to define your own delegates:
* Action: takes (up to 16) input parameters and does not return a value
* Func: takes (up to 16) input parameters and returns a single value
* Predicate: takes a single argument and returns true or false

## Examples

See `DelegatesTests`