using System;

namespace CSharpConcepts.FinalisersAndDisposal;

public class ChildWithFinaliser : ParentWithFinaliser
{
    public ChildWithFinaliser()
    {
        Console.WriteLine("Child constructor called");
    }
    
    ~ChildWithFinaliser()
    {
        Console.WriteLine("Child finaliser called");
    }
}