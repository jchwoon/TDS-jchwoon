using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enums;

public abstract class CreatureAIController<TOwner, TTarget> : BaseAIController<TOwner> where TOwner : Creature where TTarget : BaseObject
{
    public TTarget Target { get; protected set; }
    public CreatureAIController(TOwner owner) : base(owner)
    {
    }

    public abstract void FindTarget();

    protected bool IsValidTarget()
    {
        return Target != null && Target.CurrentState != EObjectState.Die;
    }
}