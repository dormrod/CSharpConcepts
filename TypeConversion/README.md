# Type Conversion

## Concept

Type conversion (or type casting), is the process to change one data type into another.
This is only possible if the types are compatible.
Type casting can be implicit or explicit.

Implicit type casting occurs automatically by the compiler when operations are performed on two compatible types.
Implicit casting can occur when there is no loss of information during the cast i.e. casting from a smaller to larger value.

Explicit type conversion can be achieved by adding a cast to the required type.
Note that explicit type conversion could lead to loss of information.
In addition, an exception may be raised if an explicit cast is not possible.

Take the example of a child type which derives from a parent. 
As the child inherits the properties from the parent, it may be safely cast to the parent.
The converse is not true by default, and an exception will be raised.
However, for a user-defined class, explicit (or implicit) casts may be defined to other types (see examples). 

There are some helper methods which help conversion to other types, for example `Convert.To`, `Parse` and `TryParse`.

## Examples

See `TypeCastingTests`