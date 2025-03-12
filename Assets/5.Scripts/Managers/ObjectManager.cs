using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

public class ObjectManager
{
    private Dictionary<int, BaseObject> _objects = new Dictionary<int, BaseObject>();
    private Dictionary<int, Monster> _monsters = new Dictionary<int, Monster>();
    private Dictionary<int, Box> _boxes = new Dictionary<int, Box>();
    public Player Player { get; private set; }

    private int _nextId = 1;

    public Monster SpawnMonster(MonsterData data)
    {
        GameObject go = Manager.Resource.Instantiate(data.prefabName, isPool : true);
        Monster monster = Utils.GetOrAddComponent<Monster>(go);
        monster.ObjectId = GenerateId();
        monster.Init(data);
        _monsters.Add(monster.ObjectId, monster);
        _objects.Add(monster.ObjectId, monster);
        return monster;
    }

    public SpawningPool SpawnSpawningPool()
    {
        int currentStage = Manager.Game.CurrentStage;
        if (Manager.Data.SpawningPoolDict.TryGetValue(currentStage, out SpawningPoolData data) == false)
            return null;

        GameObject go = Manager.Resource.Instantiate(data.prefabName);
        SpawningPool pool = go.AddComponent<SpawningPool>();
        pool.ObjectId = GenerateId();
        pool.Init(data);
        return pool;
    }

    public void RegisterBox(Box box)
    {
        box.ObjectId = GenerateId();
        _boxes.Add(box.ObjectId, box);
        _objects.Add(box.ObjectId, box);
    }

    public void RegisterPlayer(Player player)
    {
        player.ObjectId = GenerateId();
        Player = player;
        _objects.Add(player.ObjectId, player);
    }

    public void Despawn(int objectId)
    {
        BaseObject bo = FindById(objectId);
        if (bo == null)
            return;
        _objects.Remove(objectId);
        _monsters.Remove(objectId);
        _boxes.Remove(objectId);

        Manager.Resource.Destroy(bo.gameObject);
    }

    public List<Monster> GetAllMonsters()
    {
        return _monsters.Values.ToList();
    }

    public List<Box> GetAllBoxes()
    {
        return _boxes.Values.ToList();
    }

    public void Clear()
    {
        _monsters.Clear();
        _boxes.Clear();
        _nextId = 1;
    }

    public BaseObject FindById(int objectId)
    {
        BaseObject bo = null;
        if (Player && Player.ObjectId == objectId)
            return Player;
        _objects.TryGetValue(objectId, out bo);
        return bo;
    }

    private int GenerateId()
    {
        return _nextId++;
    }
}
