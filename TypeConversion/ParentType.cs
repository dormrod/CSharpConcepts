namespace CSharpConcepts.TypeConversion;

public class ParentType
{
    public int IntValue { get; set; }

    public static ParentType operator +(ParentType p, ParentType q)
        => new ParentType {IntValue = p.IntValue + q.IntValue};

    public static implicit operator int(ParentType p)
        => p.IntValue;

    public static explicit operator string(ParentType p)
        => $"'{p.IntValue}'";

    public ChildType ToChildType()
        => new ChildType {IntValue = IntValue, StrValue = (string) this};
}