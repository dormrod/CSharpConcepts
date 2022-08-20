# Dependency Injection (DI)

See Microsoft docs [here](https://docs.microsoft.com/en-us/dotnet/core/extensions/dependency-injection).

## Concept

DI is a design pattern that achieves Inversion of Control (IoC) between classes and their dependencies.
It removes the need for hard-coded dependencies between classes.
Instead, services are registered with a service container.
The service container then manages the injection of the required services into the constructor of a class when a new instance is required.

To use DI, dependencies are abstracted as interfaces in the dependent class. 
Concrete implementations are registered in the container (which can be either singletons or transient).
The concrete implementations are then passed through the constructors on instantiation of a new object.

## Advantages

The advantages of using DI are mainly around separation of concerns and assigning single responsibility:

* Replacing a dependency with a different implementation requires no modification of the dependent class. The new implementation is simply registered in place of the old one in the service container.
* Unit testing is simplified as dependencies can be easily mocked and injected.
* Configuration of complex dependency stacks is consolidated into one place.

## Examples

This directory contains two dependent classes `Dependent` and `DependentNoDi` which both implement `IDependent` 
and two dependency classes `Dependency` and `DependencyWithCounter` which both implement `IDependency`. 
These help demonstrate the difference of the DI approach (see `DependencyInjectionTests`).

1. `DependentNoDi` has a hard-coded initialisation of `Dependency` in the constructor. This cannot be swapped out for `DependencyWithCounter` without explicitly modifying the class.
2. `Dependent` has the concrete implementation of `IDependent` passed through on construction. Either implementation can be registered with the service container and injected.
3. `Dependency` returns a simple string every time its method is called.
4. `DependencyWithCounter` has an incremental counter when its method is called. This demonstrates the behaviour of the singleton vs. transient registrations.