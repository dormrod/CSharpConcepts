using System.Collections.Generic;
using System.Linq;

namespace CSharpConcepts.ExtensionMethods;

public static class EnumerableExtensions
{
    public static bool HasMoreThanNItems<T>(this IEnumerable<T> items, int n)
        => items.Count() > n;
    
    /// <summary>
    /// Convert a single item into an enumerable of one item.
    /// </summary>
    /// <remarks>
    /// If null then an empty enumerable is returned.
    /// </remarks>
    public static IEnumerable<T> Yield<T>(this T item)
    {
        if (item != null)
            yield return item;
    }
}