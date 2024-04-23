
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerializableDictionary<TKey, TValue>
{
    [SerializeField]
    private List<SerializableKeyValuePair<TKey, TValue>> keyValuePairs = new List<SerializableKeyValuePair<TKey, TValue>>();
    
    public void RemoveNullValues() => keyValuePairs.RemoveAll(pair => pair.Value == null);
    public bool ContainsKey(TKey key)
    {
        foreach (var pair in keyValuePairs)
            if (EqualityComparer<TKey>.Default.Equals(pair.Key, key))
                return true;
        return false;
    }

    public TValue this[TKey key]
    {
        get
        {
            foreach (var pair in keyValuePairs)
                if (EqualityComparer<TKey>.Default.Equals(pair.Key, key))
                    return pair.Value;
            throw new KeyNotFoundException();
        }
        set
        {
            for (int i = 0; i < keyValuePairs.Count; i++)
                if (EqualityComparer<TKey>.Default.Equals(keyValuePairs[i].Key, key))
                {
                    keyValuePairs[i] = new SerializableKeyValuePair<TKey, TValue>(key, value);
                    return;
                }
            keyValuePairs.Add(new SerializableKeyValuePair<TKey, TValue>(key, value));
        }
    }
}

[Serializable]
public struct SerializableKeyValuePair<TKey, TValue>
{
    public TKey Key;
    public TValue Value;

    public SerializableKeyValuePair(TKey key, TValue value)
    {
        Key = key;
        Value = value;
    }

    public static implicit operator SerializableKeyValuePair<TKey, TValue>(KeyValuePair<TKey, TValue> kvp)
    {
        return new SerializableKeyValuePair<TKey, TValue>(kvp.Key, kvp.Value);
    }
}
