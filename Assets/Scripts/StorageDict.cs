using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageDict<T> {
    private readonly Dictionary<T, int> storage;

    public Dictionary<T, int> Storage { get => storage; }
    public int Count { get => storage.Count; }

    public T ItemWithHighestCount {
        get {
            T result = default;
            int max = 0;

            foreach (KeyValuePair<T, int> pair in Storage) {
                if (pair.Value > max) {
                    max = pair.Value;
                    result = pair.Key;
                }
            }

            return result;
        }
    }

    public StorageDict() {
        this.storage = new Dictionary<T, int>();
    }

    public StorageDict(IEnumerable<T> items) {
        this.storage = new Dictionary<T, int>();
        AddAllItems(items);
    }

    public void AddItem(T item) {
        storage.TryGetValue(item, out int count);
        storage[item] = count + 1;
    }

    public void AddAllItems(IEnumerable<T> items) {
        foreach (T item in items) {
            AddItem(item);
        }
    }

    public T PopItem(T item) {
        if (!storage.ContainsKey(item)) {
            return default;
        }

        --storage[item];
        if (storage[item] == 0) {
            storage.Remove(item);
        }

        return item;
    }

    public bool Contains(T item) {
        return storage.ContainsKey(item);
    }
}
