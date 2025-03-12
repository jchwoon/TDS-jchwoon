using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineHelper : MonoBehaviour
{
    private static CoroutineHelper _instance;
    public static CoroutineHelper Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject("CoroutineHelper").AddComponent<CoroutineHelper>();
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }

    public new Coroutine StartCoroutine(IEnumerator routine)
    {
        return base.StartCoroutine(routine);
    }

    public new void StopCoroutine(Coroutine routine)
    {
        base.StopCoroutine(routine);
    }
}
