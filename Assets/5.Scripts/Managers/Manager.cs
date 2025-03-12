using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static bool Initialized { get; set; } = false;
    private static Manager _instance;
    private static Manager Instance
    {
        get 
        {
            ManagerInit();
            return _instance;
        }
    }

    private ResourceManager _resource = new ResourceManager();
    private DataManager _data = new DataManager();
    private PoolManager _pool = new PoolManager();
    private ObjectManager _object = new ObjectManager();
    private GameManager _game = new GameManager();

    public static ResourceManager Resource { get { return Instance._resource; } }
    public static DataManager Data { get { return Instance._data; } }
    public static PoolManager Pool { get { return Instance._pool; } }
    public static ObjectManager Object { get { return Instance._object; } }
    public static GameManager Game { get { return Instance._game; } }


    private void Awake()
    {
        ManagerInit();
    }

    private static void ManagerInit()
    {
        if (_instance != null && Initialized)
            return;

        GameObject go = GameObject.Find("Managers");
        if (go == null)
        {
            go = new GameObject("Managers");
            go.AddComponent<Manager>();
        }
        DontDestroyOnLoad(go);

        _instance = go.GetComponent<Manager>();
        Initialized = true;
    }
}
