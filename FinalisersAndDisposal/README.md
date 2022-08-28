# Finalisers and Disposal

## Concept

Finalisers (or destructors) are used to perform any necessary final clean up when a class instance is being collected by the garbage collector.

The finaliser is a parameterless method called implicitly when the class instance is destroyed, by the garbage collector (GC).
This will occur:
1. When the GC is implicitly called during program execution, and memory is full enough that the GC will identify objects to destroy
2. If the GC is explicitly called during program execution in code (`GC.Collect`)
3. At the end of the program execution, when the GC cleans up all objects

A class finaliser implicitly calls the finaliser of the base class.
The finalisers are therefore called successively up the inheritance chain. 
Finalisers should be used when the object has unmanaged resources.

However, instead of waiting for the GC to clean up such objects, it is better to explicitly release the resources when they are no longer needed.
This can be achieved through the `IDisposable` interface.
This requires an method `Dispose` which can be called when the object is no longer needed to clean up any resources. 
A `using` keyword can be used to define a scope at the end of which `Dispose` will be automatically called.

## Examples

See `FinalisersAndDisposalTests`

## Garbage Collection

See [here](https://docs.microsoft.com/en-us/dotnet/standard/garbage-collection/fundamentals?source=recommendations).

Garbage collection is “simulating a computer with an infinite amount of memory”.
It achieves this by reclaiming memory that is no longer in use.

In .NET GC enables automatic memory management, allocates objects on the heap efficiently and provides memory safety.
The GC determines which objects to collect, and when to do so.
GC usually occurs when there is low physical memory, or the heap exceeds an acceptable threshold.

On a collection, the GC determines which objects are no longer needed by the program.
To do this, it constructs a graph of accessible objects, and frees the memory of those which are not in the graph.
The graph is constructed by tracing the references from "roots" (local variables, stack, static variables).
Objects which are unreachable can be cleaned up.
The GC may then compact the memory, if a sufficiently large number of unreachable objects were removed.
It copies the surviving objects, freeing up blocks of address space, and updates their pointers.

GC does not necessarily occur on all objects in the heap.
The heap is divided into three generations of objects to improve performance: 0, 1, 2.
The younger generations are GC'd most often, and then any surviving objects may be promoted to the next generation.

Generation 0 contains short lived objects (e.g. temporary variables), and GC is conducted most frequently on this generation.
Generation 1 is a buffer between short lived and long lived objects.
Generation 2 is for long lived objects, and objects on the large object heap (default 85kb, which are too large to be regularly and efficiently moved in memory and compacted).

There is a natural trade-off between how often the GC should free memory and the performance implications of doing so.
As such, it is non-deterministic when an object will be collected if left up to the GC. 
You should release resources by disposing objects where possible.



