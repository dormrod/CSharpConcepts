using CSharpConcepts.Variables;
using NUnit.Framework;

namespace CSharpConcepts.Statics;

public class StaticsTests
{
    [Test]
    public void StaticClass_CanAccessStaticMembers()
    {
        Assert.That(StaticExample.StaticField, Is.EqualTo(42));
        Assert.That(StaticExample.StaticMethodMultiplyByTwo(4), Is.EqualTo(8));
    }
    
    [Test]
    public void InstanceClass_HasInstanceFields_SpecificToInstance()
    {
        var inst1 = new InstanceExample();
        var inst2 = new InstanceExample();
        inst1.InstanceField = -1;
        Assert.That(inst1.InstanceField, Is.EqualTo(-1)); 
        Assert.That(inst2.InstanceField, Is.EqualTo(1)); 
    }
    
    [Test]
    public void InstanceClass_HasInstanceReadonlyField_OnlySetInConstruction()
    {
        var inst1 = new InstanceExample();
        
        Assert.That(inst1.InstanceReadonlyField, Is.EqualTo(2)); 
    }
    
    [Test]
    public void InstanceClass_HasConstantVariable_SetOnInitialisation()
    {
        Assert.That(InstanceExample.ConstantField, Is.EqualTo(3)); 
    }
    
    [Test]
    public void InstanceClass_HasStaticVariable_AssociatedToType()
    {
        var inst1 = new InstanceExample();
        var inst2 = new InstanceExample();
         
        Assert.That(inst1.GetStaticSetInStaticConstructor(), Is.EqualTo(4)); 
        inst1.IncrementStaticSetInStaticConstructor();
        Assert.That(inst2.GetStaticSetInStaticConstructor(), Is.EqualTo(5));
    }
    
    [Test]
    public void InstanceClass_HasStaticVariableSetInConstructor_ReinitialisedOnNewInstance()
    {
        var inst1 = new InstanceExample();
        inst1.IncrementStaticSetInConstructor();
        
        Assert.That(inst1.GetStaticSetInConstructor(), Is.EqualTo(6));
        
        var inst2 = new InstanceExample();
        Assert.That(inst1.GetStaticSetInConstructor(), Is.EqualTo(5));
    }
    
    [Test]
    public void InstanceClass_HasStaticReadonlyVariable_OnlySetInStaticConstruction()
    {
        Assert.That(InstanceExample.StaticReadonlyField, Is.EqualTo(6)); 
    }
}