namespace CSharpConcepts.Statics;

public static class StaticExample
{
    public static readonly int StaticField;

    static StaticExample()
    {
        StaticField = 42;
    }

    public static int StaticMethodMultiplyByTwo(int n)
        => n * 2;
}