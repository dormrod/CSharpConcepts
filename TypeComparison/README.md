# Type Comparison

## Concept

If a type is a value type, two instances will be equal if the values of all the fields are the same (value equality).

If a type is a reference type, by default two instances will be equal only if the two references are the same (reference equality) 
i.e. their references point to the same object - even if all their fields are equal.

To allow reference types to be compared using value equality, the `Equals` method can be overridden. 
This can be achieved easily with the `IEquatable<T>` interface to make a tpye-safe and null-safe implementation.
In the override the field values can be compared (see examples).

In addition the `GetHashCode` method can be overridden to improve performance when using the object in a hash table
(e.g. as a key in a dictionary). 
The hash code is a integer value that relates to the state of the object.
It is not unique, but if two objects are equal they will have the same hash code.
The reverse however is not true, if two objects have the same hash code they will not necessarily be equal.
Further, if two objects have different hash codes they will not be equal.

The hash code allows objects to be bucketed with an average time complexity of O(1).
A good hashing algorithm should uniformly distribute objects across all integer buckets,
to reduce the number of objects that have to be searched in each bucket (see examples).

## Examples

A simple set of example types are set up with an integer and string field.

1. `ValueType`: simple value type
2. `ReferenceType`: simple reference type
3. `ReferenceTypeEquatable`: reference type with `Equals` overridden
4. `ReferenceTypeGoodHashCode`: reference type with sensible override for `GetHashCode` which evenly distributes objects
5. `ReferenceTypeBadHashCode`: reference type with naive override for `GetHashCode` which picks from the set {0, 1, 2} for the code

See `TypeComparisonTests` for demonstrations of behaviour.