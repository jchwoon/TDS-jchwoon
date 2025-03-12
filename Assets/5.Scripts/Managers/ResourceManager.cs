using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    private Dictionary<string, UnityEngine.Object> _resources = new Dictionary<string, UnityEngine.Object>();

    public T GetResource<T>(string key) where T : UnityEngine.Object
    {
        if (_resources.TryGetValue(key, out UnityEngine.Object obj))
        {
            return obj as T;
        }

        return null;
    }

    public GameObject Instantiate(string key, Transform parent = null, bool isPool = false)
    {
        GameObject prefab = GetResource<GameObject>(key);

        if (prefab == null)
            return null;

        if (isPool == true)
        {
            GameObject poolGo = Manager.Pool.Pop(prefab, parent);
            if (poolGo != null)
                return poolGo;
        }

        GameObject go = UnityEngine.Object.Instantiate(prefab, parent);

        go.name = prefab.name;
        return go;
    }

    public void Destroy(GameObject go)
    {
        if (go == null)
            return;

        if (Manager.Pool.Push(go) == true)
            return;

        UnityEngine.Object.Destroy(go);
    }

    public void LoadAll()
    {
        UnityEngine.Object[] objs = Resources.LoadAll<UnityEngine.Object>("PreLoad");
        foreach (UnityEngine.Object obj in objs)
        {
            _resources.Add(obj.name, obj);
        }
    }
}
