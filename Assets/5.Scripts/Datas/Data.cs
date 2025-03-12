using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DataManager;
using static Enums;

namespace Data
{
    [Serializable]
    public class BaseData
    {
        public string prefabName;
    }

    [Serializable]
    public class PlayerData : BaseData
    {
        public EPlayerType playerType;
        public int maxHp;
        public int attackDamage;
        public int defence;
        public int attackSpeed;
        public int skillTemplateId;
    }

    [Serializable]
    public class MonsterData : BaseData
    {
        public int templateId;
        public int maxHp;
        public int moveSpeed;
        public int attackDamage;
        public int defence;
        public int attackSpeed;
        public float attackRange;
        public string name;
        public int skillTemplateId;
    }

    [Serializable]
    public class MonsterDataLoader : IDataLoader<int, MonsterData>
    {
        public List<MonsterData> monsters = new List<MonsterData>();
        public Dictionary<int, MonsterData> MakeDict()
        {
            Dictionary<int, MonsterData> dict = new Dictionary<int, MonsterData>();

            foreach (MonsterData monster in monsters)
            {
                dict.Add(monster.templateId, monster);
            }

            return dict;
        }
    }

    [Serializable]
    public class PoolData
    {
        public int monsterTemplateId;
        public int spawnDelay;
    }

    [Serializable]
    public class SpawningPoolData : BaseData
    {
        public int stage;
        public float posX;
        public float posY;
        public List<PoolData> poolDatas;
    }

    [Serializable]
    public class  SpawningPoolDataLoader : IDataLoader<int, SpawningPoolData>
    {
        public List<SpawningPoolData> spawningPools = new List<SpawningPoolData>();
        public Dictionary<int, SpawningPoolData> MakeDict()
        {
            Dictionary<int, SpawningPoolData> dict = new Dictionary<int, SpawningPoolData>();

            foreach (SpawningPoolData spawningPool in spawningPools)
            {
                dict.Add(spawningPool.stage, spawningPool);
            }

            return dict;
        }
    }

    [Serializable]
    public class StageData
    {
        public int stage;
        public string name;
        public float stageDuration;
    }

    [Serializable]
    public class StageDataLoader : IDataLoader<int, StageData>
    {
        public List<StageData> stages = new List<StageData>();
        public Dictionary<int, StageData> MakeDict()
        {
            Dictionary<int, StageData> dict = new Dictionary<int, StageData>();

            foreach (StageData stage in stages)
            {
                dict.Add(stage.stage, stage);
            }

            return dict;
        }
    }

    [Serializable]
    public class SkillData
    {
        public int templateId;
        public ESkillAreaType skillAreaType;
        public string animName;
        public float skillRange;
        public float animTime;
        public float castingTime;
        public string animParamName;
        public int maxHitCount;
        public float sectorAngle;
    }

    [Serializable]
    public class SkillDataLoader : IDataLoader<int, SkillData>
    {
        public List<SkillData> skills = new List<SkillData>();
        public Dictionary<int, SkillData> MakeDict()
        {
            Dictionary<int, SkillData> dict = new Dictionary<int, SkillData>();

            foreach (SkillData skill in skills)
            {
                dict.Add(skill.templateId, skill);
            }

            return dict;
        }
    }
}
