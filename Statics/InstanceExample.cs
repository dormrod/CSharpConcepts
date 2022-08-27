using System;
using System.Diagnostics;
using NUnit.Framework;

namespace CSharpConcepts.Variables;

public class InstanceExample
{
    public int InstanceField;
    public readonly int InstanceReadonlyField;
    
    public const int ConstantField = 3;

    private static int _staticFieldSetInStaticConstructor;
    private static int _staticFieldSetInConstructor;
    public static readonly int StaticReadonlyField;

    static InstanceExample()
    {
        _staticFieldSetInStaticConstructor = 4;
        StaticReadonlyField = 6;
    }

    public InstanceExample()
    {
        InstanceField = 1;
        InstanceReadonlyField = 2;
        _staticFieldSetInConstructor = 5;
    }

    public void IncrementStaticSetInStaticConstructor()
        => ++_staticFieldSetInStaticConstructor;

    public int GetStaticSetInStaticConstructor()
        => _staticFieldSetInStaticConstructor;
    
    public void IncrementStaticSetInConstructor()
        => ++_staticFieldSetInConstructor;

    public int GetStaticSetInConstructor()
        => _staticFieldSetInConstructor;
}