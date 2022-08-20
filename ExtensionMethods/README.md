# Extension Methods

See Microsoft docs [here](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/extension-methods).

## Concept

Extension methods provide a way to add additional methods to existing types, without needing to create a derived type or modifying the original type.
This can be useful if you don't own the underlying type.
In terms of implementation, extension methods are just static methods, but which can be invoked as if they are methods on the underlying class.

## Examples

This directory contains `EnumerableExtensions` with two extension methods:

1. `HasMoreThanNItems` returns true if an enumerable contains more than the specified number of items
2. `Yield` is a useful method that returns an enumerable of a single item from an object

The usage of these is called out in `EnumerableExtensionTests`.