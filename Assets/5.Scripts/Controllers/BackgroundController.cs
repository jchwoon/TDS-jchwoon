using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public float startPos = 80.0f;
    public float endPos = -80.0f;
    public float spawnPos = 30.0f;

    public float speed = 5.0f;

    private bool _isSpawnedNext = false;

    void Update()
    {
        float distPerTick = speed * Time.deltaTime;
        transform.Translate(Vector3.left * distPerTick);
        if (_isSpawnedNext == false && transform.position.x < spawnPos)
        {
            SpawnNextBackgroundGroup();
        }
        if (transform.position.x < endPos)
        {
            Manager.Resource.Destroy(gameObject);
        }
    }

    public void SpawnNextBackgroundGroup()
    {
        GameObject go = Manager.Resource.Instantiate("BackGroundGroup");
        go.transform.position = new Vector3(startPos, 0, 0);
        _isSpawnedNext = true;
    }
}
