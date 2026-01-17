using System.Collections.Generic;
using UnityEngine;

public static class ObjectPoolManager
{
    private static readonly Dictionary<GameObject, Stack<GameObject>> pools = new();
    private static readonly Dictionary<GameObject, GameObject> instanceToSource = new();

    private static Transform root;

    private static Transform Root
    {
        get
        {
            if (root == null)
            {
                var go = new GameObject("Pool");
                Object.DontDestroyOnLoad(go);
                root = go.transform;
            }
            return root;
        }
    }

    // LeanPool.Spawn(go)
    public static GameObject Spawn(GameObject source, Vector3 pos = default, Quaternion rot = default, Transform parent = null)
    {
        if (source == null)
        {
            Debug.LogError("[ObjectPoolManager] Cannot spawn null source!");
            return null;
        }

        if (!pools.TryGetValue(source, out var pool))
        {
            pool = new Stack<GameObject>();
            pools[source] = pool;
        }

        GameObject obj = null;
        
        while (pool.Count > 0 && obj == null)
        {
            var candidate = pool.Pop();
            if (candidate != null)
            {
                obj = candidate;
            }
            else
            {
                Debug.LogWarning($"[ObjectPoolManager] Found destroyed object in pool for {source.name}, skipping...");
            }
        }

        if (obj == null)
        {
            obj = Object.Instantiate(source);
            instanceToSource[obj] = source;
        }

        if (obj == null)
        {
            Debug.LogError("[ObjectPoolManager] Failed to create object from source!");
            return null;
        }

        var t = obj.transform;
        t.SetParent(parent, false);
        t.SetPositionAndRotation(pos, rot);

        obj.SetActive(true);
        return obj;
    }

    // LeanPool.Despawn(this)
    public static void Despawn(GameObject obj)
    {
        if (!instanceToSource.TryGetValue(obj, out var source))
        {
            Object.Destroy(obj);
            return;
        }

        obj.SetActive(false);
        obj.transform.SetParent(Root);
        pools[source].Push(obj);
    }

    // Despawn all active pooled objects of a specific type
    public static void DespawnAll<T>() where T : Component
    {
        var activeInstances = new List<GameObject>(instanceToSource.Keys);
        
        foreach (var instance in activeInstances)
        {
            if (instance != null && instance.activeInHierarchy && instance.TryGetComponent<T>(out _))
            {
                Despawn(instance);
            }
        }
    }
}