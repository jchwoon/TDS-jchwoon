using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enums;

public class Box : BaseObject
{
    const int MAX_DAMAGE_TAKEN = 10000;
    private int _currentDamageTaken = 0;
    protected override void Awake()
    {
        Manager.Object.RegisterBox(this);
        AIController = new BaseAIController<BaseObject>(this);
    }

    public override void OnDamage(float damage)
    {
        _currentDamageTaken++;

        if (_currentDamageTaken >= MAX_DAMAGE_TAKEN)
        {
            OnDie();
        }
    }

    public override void OnDie()
    {
        AIController.ChangeState(EObjectState.Die);
        Manager.Object.Despawn(ObjectId);
    }
}
