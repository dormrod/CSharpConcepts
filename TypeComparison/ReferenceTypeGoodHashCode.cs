using System;

namespace CSharpConcepts.TypeComparison;

public class ReferenceTypeGoodHashCode : ReferenceType, IEquatable<ReferenceTypeGoodHashCode>
{
    public bool Equals(ReferenceTypeGoodHashCode? other)
    {
        if (other == null)
            return false;

        return IntValue == other.IntValue
               && StrValue == other.StrValue;
    }

    public override int GetHashCode()
    {
        int hash = 23;
        hash = hash * 37 + IntValue.GetHashCode();
        hash = hash * 37 + StrValue.GetHashCode();
        return hash;
    }
}