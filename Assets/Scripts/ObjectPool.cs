using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : Component
{
    private Queue<T> pool = new Queue<T>();
    private T prefab;
    private Transform container;

    public ObjectPool(T prefab, int initialCount, Transform container)
    {
        this.prefab = prefab;
        this.container = container;

        for (int i = 0; i < initialCount; i++)
        {
            T obj = CreateNewObject();
            obj.gameObject.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    private T CreateNewObject()
    {
        T obj = Object.Instantiate(prefab, container);
        return obj;
    }

    public T Get()
    {
        if (pool.Count == 0)
        {
            T obj = CreateNewObject();
            return obj;
        }
        else
        {
            T obj = pool.Dequeue();
            //obj.gameObject.SetActive(true);
            return obj;
        }
    }

    public void Return(T obj)
    {
        obj.gameObject.SetActive(false);
        pool.Enqueue(obj);
    }
}
