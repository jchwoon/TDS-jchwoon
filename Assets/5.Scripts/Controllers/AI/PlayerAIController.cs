using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enums;

public class PlayerAIController : CreatureAIController<Player, Monster>
{
    public PlayerAIController(Player owner) : base(owner)
    {
        ChangeState(EObjectState.Skill);
    }

    public override void FindTarget()
    {
        List<BaseObject> objects = new List<BaseObject>();
        objects.AddRange(Manager.Object.GetAllMonsters());
        float minDistance = float.MaxValue;
        foreach (BaseObject obj in objects)
        {
            float sqrDistance = (Owner.transform.position - obj.transform.position).sqrMagnitude;
            if (sqrDistance < minDistance)
            {
                minDistance = sqrDistance;
                Target = obj as Monster;
            }
        }
    }

    public override void Update()
    {
        FindTarget();
        base.Update();
    }

    protected override void UpdateSkill()
    {
        base.UpdateSkill();
        if (Owner.SkillComponent.IsSkillUsing)
            return;

        if (IsValidTarget() == false)
            return;

        Owner.SkillComponent.CheckAndUseSkill(Target);
    }
}