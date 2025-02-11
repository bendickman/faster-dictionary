using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using FasterDictionary.Client.Extensions;

namespace FasterDictionary.Client.Benchmarks;

[SimpleJob(runtimeMoniker: RuntimeMoniker.Net80)]
public class DictionaryBenchmarks
{
    [Benchmark]
    public int StandardDictionaryGetNewValue()
    {
        var dictionary = new Dictionary<string, int>();

        if (!dictionary.TryGetValue("1", out var value))
        {
            dictionary["1"] = 1;
        }

        return dictionary["1"];
    }

    [Benchmark]
    public int CollectionsMarshalDictionaryGetNewValue()
    {
        var dictionary = new Dictionary<string, int>();

        return dictionary.GetOrAdd("1", 1);
    }

    [Benchmark]
    public int StandardDictionaryGetExistingValue()
    {
        var dictionary = new Dictionary<string, int>()
        {
            { "1", 1 },
        };

        if (!dictionary.TryGetValue("1", out var value))
        {
            dictionary["1"] = 1;
        }

        return dictionary["1"];
    }

    [Benchmark]
    public int CollectionsMarshalDictionaryGetExistingValue()
    {
        var dictionary = new Dictionary<string, int>()
        {
            { "1", 1 },
        };

        return dictionary.GetOrAdd("1", 1);
    }

    /////////////////////////////////////
    ///
    [Benchmark]
    public bool StandardDictionaryUpdateNewValue()
    {
        var dictionary = new Dictionary<string, int>();

        if (!dictionary.TryGetValue("1", out var value))
        {
            return false;
        }

        dictionary["1"] = 2;
        return true;
    }

    [Benchmark]
    public bool CollectionsMarshalDictionaryUpdateNewValue()
    {
        var dictionary = new Dictionary<string, int>();

        return dictionary.TryUpdate("1", 2);
    }

    [Benchmark]
    public bool StandardDictionaryUpdateExistingValue()
    {
        var dictionary = new Dictionary<string, int>()
        {
            { "1", 1 },
        };

        if (!dictionary.TryGetValue("1", out var value))
        {
            return false;
        }

        dictionary["1"] = 2;
        return true;
    }

    [Benchmark]
    public bool CollectionsMarshalDictionaryUpdateExistingValue()
    {
        var dictionary = new Dictionary<string, int>()
        {
            { "1", 1 },
        };

        return dictionary.TryUpdate("1", 2);
    }
}
