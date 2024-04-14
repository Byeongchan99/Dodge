using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPoolManager : MonoBehaviour
{
    public static EffectPoolManager Instance { get; private set; }

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

    private Dictionary<string, ObjectPool<BaseEffect>> pools = new Dictionary<string, ObjectPool<BaseEffect>>();

    public void CreatePool<T>(T prefab, int initialCount, Transform container) where T : BaseEffect
    {
        string poolName = typeof(T).Name;
        if (!pools.ContainsKey(poolName))
        {
            pools[poolName] = new ObjectPool<BaseEffect>(prefab, initialCount, container);
        }
    }

    public BaseEffect Get(string poolName)
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

    public void Return(string poolName, BaseEffect obj)
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
