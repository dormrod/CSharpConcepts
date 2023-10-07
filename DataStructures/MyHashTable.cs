using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpConcepts.DataStructures;

public class MyKeyValuePair<TKey, TValue>
{
    public TKey Key { get; }
    public TValue Value { get; }

    public MyKeyValuePair(TKey key, TValue value)
    {
        Key = key;
        Value = value;
    }
}

public class MyBucket<TKey, TValue>
{
    // Cheating - but this is to handle collisions easily instead of doing a second hashing
    // e.g. h(key, n) = h1(key) + n*h2(key), where n is the number of collisions
    private readonly List<MyKeyValuePair<TKey, TValue>> _keyValuePairs;

    public MyBucket()
    {
        _keyValuePairs = new List<MyKeyValuePair<TKey, TValue>>();
    }

    public void Add(MyKeyValuePair<TKey, TValue> keyValuePair)
    {
        if (_keyValuePairs.Any(kvp => kvp.Key.Equals(keyValuePair.Key)))
            throw new InvalidOperationException($"Key with value {keyValuePair.Key} has already been added.");
        
        _keyValuePairs.Add(keyValuePair);
    }

    public TValue Get(TKey key)
    {
        var keyValuePair = _keyValuePairs.SingleOrDefault(kvp => kvp.Key.Equals(key));

        if (keyValuePair == null)
            throw new InvalidOperationException($"No value with key {key}.");

        return keyValuePair.Value;
    }
    
    public void Remove(TKey key)
    {
        var keyValuePair = _keyValuePairs.SingleOrDefault(kvp => kvp.Key.Equals(key));

        if (keyValuePair == null)
            throw new InvalidOperationException($"No value with key {key}.");

        _keyValuePairs.Remove(keyValuePair);
    }

    public void Clear()
    {
        _keyValuePairs.Clear();
    }
}

public class MyHashTable<TKey, TValue>
{
    private readonly MyBucket<TKey, TValue>[] _buckets;

    public MyHashTable(int buckets)
    {
        _buckets = new MyBucket<TKey, TValue>[buckets];
        for (int i = 0; i < buckets; ++i)
        {
            _buckets[i] = new MyBucket<TKey, TValue>();
        }
    }

    public TValue this[TKey key] => Get(key);

    public void Add(TKey key, TValue value)
    {
        if (key == null)
            throw new NullReferenceException("Key cannot be null");
        
        var kvp = new MyKeyValuePair<TKey, TValue>(key, value);
        var bucketIndex = Math.Abs(kvp.Key.GetHashCode() % _buckets.Length);
        
        _buckets[bucketIndex].Add(kvp);
    }

    public TValue Get(TKey key)
    {
        if (key == null)
            throw new NullReferenceException("Key cannot be null");
        
        var bucketIndex = Math.Abs(key.GetHashCode() % _buckets.Length);

        return _buckets[bucketIndex].Get(key);
    }
    
    public void Remove(TKey key)
    {
        if (key == null)
            throw new NullReferenceException("Key cannot be null");
        
        var bucketIndex = Math.Abs(key.GetHashCode() % _buckets.Length);

        _buckets[bucketIndex].Remove(key);
    }

    public void Clear()
    {
        foreach (var bucket in _buckets)
        {
            bucket.Clear();
        }
    }
}