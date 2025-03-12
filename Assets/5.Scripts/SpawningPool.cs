using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningPool : BaseObject
{
    public SpawningPoolData SpawningPoolData { get; set; }
    private List<Coroutine> _coroutines = new List<Coroutine>();

    public void Init(SpawningPoolData data)
    {
        SpawningPoolData = data;
        transform.position = new Vector2(data.posX, data.posY);

        foreach (PoolData poolData in SpawningPoolData.poolDatas)
        {
            _coroutines.Add(StartCoroutine(CoSpawnRoutine(poolData)));
        }
    }

    private IEnumerator CoSpawnRoutine(PoolData poolData)
    {
        while (true)
        {
            Spawn(poolData);
            yield return new WaitForSeconds(poolData.spawnDelay);
        }
    }

    private void Spawn(PoolData poolData)
    {
        if (Manager.Data.MonsterDict.TryGetValue(poolData.monsterTemplateId, out MonsterData monsterData) == false)
            return;

        Monster monster = Manager.Object.SpawnMonster(monsterData);
        monster.SetPos(new Vector2(SpawningPoolData.posX, SpawningPoolData.posY));
    }

    public void Clear()
    {
        foreach (Coroutine coroutine in _coroutines)
        {
            StopCoroutine(coroutine);
        }

        _coroutines.Clear();
    }
}
