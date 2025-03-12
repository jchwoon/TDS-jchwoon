using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enums;

public class StatInfo
{
    public int MaxHp;
    public int Hp;
    public int AttackDamage;
    public int Defence;
    public float MoveSpeed;
    public float AttackSpeed;
    public float AttackRange;
}

public class StatComponent
{
    public BaseObject Owner { get; private set; }
    public StatInfo StatInfo { get; private set; }
    private Dictionary<EStatType, Func<StatInfo, float>> GetStatDict = new Dictionary<EStatType, Func<StatInfo, float>>()
    {
        { EStatType.MaxHp, (info) => info.MaxHp },
        { EStatType.Hp, (info) => info.Hp },
        { EStatType.AttackDamage, (info) => info.AttackDamage },
        { EStatType.Defence, (info) => info.Defence },
        { EStatType.MoveSpeed, (info) => info.MoveSpeed },
        { EStatType.AttackSpeed, (info) => info.AttackSpeed },
        { EStatType.AttackRange, (info) => info.AttackRange }
    };

    private Dictionary<EStatType, Action<StatInfo, float>> SetStatDict = new Dictionary<EStatType, Action<StatInfo, float>>()
    {
        { EStatType.MaxHp, (info, value) => info.MaxHp = (int)value },
        { EStatType.Hp, (info, value) => info.Hp = (int)value },
        { EStatType.AttackDamage, (info, value) => info.AttackDamage = (int)value },
        { EStatType.Defence, (info, value) => info.Defence = (int)value },
        { EStatType.MoveSpeed, (info, value) => info.MoveSpeed = value },
        { EStatType.AttackSpeed, (info, value) => info.AttackSpeed = value },
        { EStatType.AttackRange, (info, value) => info.AttackRange = value }
    };

    public StatComponent(BaseObject owner)
    {
        Owner = owner;
    }

    public void Init(StatInfo info)
    {
        StatInfo = new StatInfo()
        {
            MaxHp = info.MaxHp,
            Hp = info.Hp,
            AttackDamage = info.AttackDamage,
            Defence = info.Defence,
            MoveSpeed = info.MoveSpeed,
            AttackSpeed = info.AttackSpeed,
            AttackRange = info.AttackRange
        };
    }

    public void SetStat(EStatType statType, float value)
    {
        SetStatDict[statType].Invoke(StatInfo, value);
    }

    public float GetStat(EStatType statType)
    {
        return GetStatDict[statType].Invoke(StatInfo);
    }
}
