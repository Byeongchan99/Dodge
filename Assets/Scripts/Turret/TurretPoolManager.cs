using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretPoolManager : MonoBehaviour
{
    public static TurretPoolManager Instance { get; private set; }

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

    private Dictionary<string, ObjectPool<BaseTurret>> pools = new Dictionary<string, ObjectPool<BaseTurret>>();

    public void CreatePool<T>(T prefab, int initialCount, Transform container) where T : BaseTurret
    {
        string poolName = typeof(T).Name;
        if (!pools.ContainsKey(poolName))
        {
            pools[poolName] = new ObjectPool<BaseTurret>(prefab, initialCount, container);
        }
    }

    public BaseTurret Get(string poolName)
    {
        if (pools.ContainsKey(poolName))
        {
            return pools[poolName].Get();
        }
        else
        {
            Debug.LogWarning($"Pool with name {poolName} doesn't exist.");
            return null;
        }
    }

    public void Return(string poolName, BaseTurret obj)
    {
        if (pools.ContainsKey(poolName))
        {
            pools[poolName].Return(obj);
        }
        else
        {
            Debug.LogWarning($"Pool with name {poolName} doesn't exist.");
        }
    }
}
