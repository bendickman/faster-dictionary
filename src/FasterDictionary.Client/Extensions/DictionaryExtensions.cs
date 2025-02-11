using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace FasterDictionary.Client.Extensions;

public static class DictionaryExtensions
{
    public static TValue? GetOrAdd<TValue, TKey>(
        this Dictionary<TKey, TValue> dictionary,
        TKey key,
        TValue? value)
        where TKey : notnull
    {
        ref var val = ref CollectionsMarshal.GetValueRefOrAddDefault(dictionary, key, out var exists);

        if (exists)
        {
            return val;
        }

        val = value;

        return val;
    }

    public static bool TryUpdate<TKey, TValue>(
        this Dictionary<TKey, TValue> dictionary,
        TKey key,
        TValue value)
        where TKey : notnull
    {
        ref var val = ref CollectionsMarshal.GetValueRefOrNullRef(dictionary, key);

        if (Unsafe.IsNullRef(ref val))
        {
            return false;
        }

        val = value;

        return true;
    }
}
