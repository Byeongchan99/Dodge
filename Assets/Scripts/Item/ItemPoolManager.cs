using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPoolManager : MonoBehaviour
{
    public static ItemPoolManager Instance { get; private set; }

    void Awake()
    {
        // ΩÃ±€≈Ê ¿ŒΩ∫≈œΩ∫ º≥¡§
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private Dictionary<string, ObjectPool<BaseItem>> pools = new Dictionary<string, ObjectPool<BaseItem>>();

    public void CreatePool<T>(T prefab, int initialCount, Transform container) where T : BaseItem
    {
        string poolName = typeof(T).Name;
        if (!pools.ContainsKey(poolName))
        {
            pools[poolName] = new ObjectPool<BaseItem>(prefab, initialCount, container);
        }
    }

    public BaseItem Get(string poolName)
    {
        if (pools.ContainsKey(poolName))
        {
            return pools[poolName].Get();
        }
        else
        {
            Debug.LogWarning($"Get Pool with name {poolName} doesn't exist.");
            return null;
        }
    }

    public void Return(string poolName, BaseItem obj)
    {
        if (pools.ContainsKey(poolName))
        {
            pools[poolName].Return(obj);
        }
        else
        {
            Debug.LogWarning($"Return Pool with name {poolName} doesn't exist.");
        }
    }
}
