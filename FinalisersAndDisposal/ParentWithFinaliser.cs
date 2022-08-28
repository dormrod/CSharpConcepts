using System;

namespace CSharpConcepts.FinalisersAndDisposal;

public class ParentWithFinaliser
{
    public ParentWithFinaliser()
    {
        Console.WriteLine("Parent constructor called");
    }
    
    ~ParentWithFinaliser()
    {
        Console.WriteLine("Parent finaliser called");
    }
}