# Statics

## Concept

The static keyword indicates that a member (method, field, property, constructor etc.) belongs to the type itself and not any specific object.

No instance of a class has to be created in order for static members called, and a static class can not be instantiated.
In addition a static class is sealed (cannot be inherited from) and does not have an instance constructor.

Classes are guaranteed to be loaded, have the static members initialised and the static constructor called before the class is referenced for the first time.
Static classes must have all static members.
Non-static classes can have a mix of static and non-static members.

There are therefore different kinds of member variable which may be in a class:

1. Instance variable: memory is allocated whenever an instance of the class is created, there will be a unique address for the variable of each class
2. Static variable: memory is allocated when the class is loaded, there is only one memory address for all instance of the class
3. Readonly variables: value is set on construction and cannot be changed
4. Constant variables: memory is allocated when the class is loaded, value is set at initialisation and it cannot be changed

Static variables can be initialised in the static constructor the first time the class is instantiated. 
They *could* be set in the constructor of a class, but these would be re-initialised every time an object is created,
and so in the majority of cases this would be a very bad idea!

## Examples

See `StaticsTests`