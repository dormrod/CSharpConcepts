using System.Linq;

namespace CSharpConcepts.Delegates;

public static class ExampleFunctions
{
    public static int Counter = 0;

    public static string Reflect(string s)
        => $"{s}{string.Join(string.Empty, s.Reverse().Select(c => c))}";
    
    public static string TakeEveryOtherLetter(string s)
        => string.Join(string.Empty,
            Enumerable.Range(0, s.Length)
                .Where(i => i % 2 == 0)
                .Select(i => s[i]));

    public static void ResetCounter()
        => Counter = 0;
    
    public static void AddOneToCounter()
        => ++Counter;

    public static void AddTenToCounter()
        => Counter += 10;
}