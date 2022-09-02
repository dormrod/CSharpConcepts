# Types

## Concept

Data types are used to store information in a program. Different data types define:

1. Size of the memory location
2. Range of allowed data which can be stored
3. Operations which can be performed
4. Types of results coming out of expressions using these types

There are three kinds of types in C#:

1. Value types
2. Reference types
3. Pointer types

### Value Types

Value types store their values directly in memory.
There are pre-defined value types such as `int`, `float`, `long`, `double`, `bool` etc. 
as well as user defined value types `structs` and `enums`.

These types store their values in memory. 
On a computer data is ultimately stored as binary numbers called bits (0/1).
Eight bits together form a byte, which gives 2^8 = 256 possible values, from 0-255.

There is a basic .NET type, `Byte`, to represent these 8-bit unsigned integer values.
These can be converted to say the 255 ASCII characters. 
There is another type `Char` which is a two-byte (16-bit) unsigned integer.
A `Char` has a range of 2^16 = 65536 possible values. 
This can be used to store UTF-16 characters.
A series of `Char` can be built up to form strings (see later).

There are also numeric types. Integer types come in `Int16` (short), `Int32` (int) and `Int64` (long) types,
containing 16, 32 or 64 bits. These increasingly use more memory (with an impact on performance) but can store larger values.
These are signed data types, in that the values can be positive or negative. 
As such, a single bit is needed to represent the sign, leaving the magnitude as 2^N-1
e.g. `Int16` has a range of -32768 to 32767. 

Floating point numbers come in `Single` (4 byte), `Double` (8 byte) and `Decimal` (16 bytes) types,
which handle increasing levels of precision.

All value types have a default value, which is `0` or equivalent.

### Reference Types

Reference types do not store the actual values for a variable directly, but rather a reference to the address where the value is stored.
The reference type contains a pointer to another memory location that holds the data (the referenced object).
                                                                                  
When reference types are passed as parameters or assigned to variables, it is the reference, not the referenced object which is stored.

Reference types have a default `null` value i.e. they do not point to any object. 

N.B. a `string` is a reference type but is immutable, so its value cannot be changed once assigned. 
Any apparent mutability comes from creating a new string object.

### Pointer Types 

A pointer stores the memory address of another variable.

It is used in conjunction with the address operator `&` which gets the memory address of a variable, and the indication operator `*` which access the value of an address.
                                 
In C# a pointer can only be declared to hold the memory address of value types and arrays. 
Pointer types are not tracked by the default garbage collection mechanism, and their use leads to unsafe code.

### Call By Value And By Reference

Note that by default all variables passed to functions will be by value and not by reference, regardless of whether the parameter types are value or reference types.

What this means is that a copy of the original variable is passed as a parameter to the method.
The memory location referred to by the parameters and actual arguments is different.
Any changes to the variable in the method will therefore not change the original argument.

The key is that for reference types, this will not change the original passed reference. 
However, if the copied reference is used to modify the associated object, 
the data will have changed, even if this is subsequently accessed by the original reference.

If the `ref` keyword is used, the actual memory address is passed into the function, such that the parameter and the variable passed in refer to the same object.
Any changes made in the method will be reflected in the original argument.

### Stack And Heap Memory in C#

There are two different places where memory can be allocated in RAM when a variable is declared in .NET - the stack and the heap.

The stack is a last-in-first-out data structure, which temporarily stores fixed-size items relating to the currently executing function call stack
i.e. function arguments and local variables, in a contiguous memory block. The memory allocation and deallocation is automatically managed.
The amount of memory to be allocated when a function is called is known by the compiler, and after the method is complete the stack frame is popped freeing the memory.
There is one stack per thread, and it is relatively fast to access.

The heap is a less structured region of memory, for dynamic memory allocation, which can be accessed in any order.
Memory is not automatically deallocated in the heap, instead a garbage collector is required to periodically clean it up.
Furthermore, the heap may become fragmented as items are deleted, requiring defragmentation.
There is only one heap per program, and all threads have access to the heap.
                  
In terms of where different items are stored:
1. A reference type (i.e. the referenced object) always is always allocated on the heap
2. Value types and the reference for a reference type can go on the stack or the heap, depending where they were declared
   1. If declared as a local variable in a method or as a parameter, they go on the stack
   2. If they are a member on a class then they are stored on the heap with the reference type 
   3. If they are a member of a struct, then they are stored wherever the struct is stored

Once there are no remaining references to an object on the heap, it is marked for disposal by the garbage collector.

See Finalisers and Disposal for more.

### Boxing and Unboxing

Value types can be converted to reference types (and vice versa) using boxing (and unboxing).

This occurs on casting a value type to `object`, and the `object` back to a value type.
There is a performance overhead to this.