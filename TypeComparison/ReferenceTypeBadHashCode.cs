using System;

namespace CSharpConcepts.TypeComparison;

public class ReferenceTypeBadHashCode : ReferenceType, IEquatable<ReferenceTypeBadHashCode>
{
    public bool Equals(ReferenceTypeBadHashCode? other)
    {
        if (other == null)
            return false;

        return IntValue == other.IntValue
               && StrValue == other.StrValue;
    }

    public override int GetHashCode()
        => IntValue % 3;
}