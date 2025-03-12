using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enums 
{
    public enum EObjectState
    {
        Move,
        Skill,
        Die
    }

    public enum ESkillAreaType
    {
        None = 1,
        Corn
    }

    public enum EStatType
    {
        MaxHp,
        Hp,
        AttackDamage,
        Defence,
        MoveSpeed,
        AttackSpeed,
        AttackRange
    }

    public enum ELayermask
    {
        Bottom = 6,
        Middle = 7,
        Top = 8,
        Truck = 9
    }

    public enum ESortingLayer
    {
        Bottom = 1,
        Middle = 2,
        Top = 3
    }

    public enum EBoundsEdge
    {
        Top,
        Bottom,
        Left,
        Right
    }

    public enum EPlayerType
    {
        Normal = 1
    }
}
