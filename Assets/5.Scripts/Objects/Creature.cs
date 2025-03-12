using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : BaseObject
{
    public SkillComponent SkillComponent { get; protected set; }
    protected override void Awake()
    {
        base.Awake();
    }

    protected virtual void SetStatInfo(StatInfo info)
    {
        StatComponent.Init(info);
    }

    protected virtual void SetSkillInfo(int skillTemplateId)
    {
        SkillComponent.RegisterSkill(skillTemplateId);
    }
}
