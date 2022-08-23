using System;

namespace CSharpConcepts.TypeComparison;

public class ReferenceTypeEquatable : ReferenceType, IEquatable<ReferenceTypeEquatable>
{
    public bool Equals(ReferenceTypeEquatable? other)
    {
        if (other == null)
            return false;

        return IntValue == other.IntValue
               && StrValue == other.StrValue;
    }
}