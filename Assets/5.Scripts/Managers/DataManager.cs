using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enums;

public class DataManager
{
    public interface IDataLoader<Key, Value>
    {
        public Dictionary<Key, Value> MakeDict();
    }

    public Dictionary<int, MonsterData> MonsterDict { get; private set; } = new Dictionary<int, MonsterData>();
    public Dictionary<int, SpawningPoolData> SpawningPoolDict { get; private set; } = new Dictionary<int, SpawningPoolData>();
    public Dictionary<int, StageData> StageDict { get; private set; } = new Dictionary<int, StageData>();
    public Dictionary<int, SkillData> SkillDict { get; private set; } = new Dictionary<int, SkillData>();



    public void LoadData()
    {
        MonsterDict = LoadJson<int, MonsterData, MonsterDataLoader>("MonsterData").MakeDict();
        SpawningPoolDict = LoadJson<int, SpawningPoolData, SpawningPoolDataLoader>("SpawningPoolData").MakeDict();
        StageDict = LoadJson<int, StageData, StageDataLoader>("StageData").MakeDict();
        SkillDict = LoadJson<int, SkillData, SkillDataLoader>("SkillData").MakeDict();
    }

    public Loader LoadJson<Key, Value, Loader>(string key) where Loader : IDataLoader<Key, Value>
    {
        TextAsset textAsset = Manager.Resource.GetResource<TextAsset>(key);
        Loader loader =  JsonUtility.FromJson<Loader>(textAsset.text);
        return loader;
    }
}
