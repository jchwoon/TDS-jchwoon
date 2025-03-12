using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enums;

public class BaseObject : MonoBehaviour
{
    public AnimationData AnimData { get; private set; }
    public Animator Animator { get; protected set; }
    public StatComponent StatComponent { get; protected set; }
    public Rigidbody2D Rigidbody { get; protected set; }
    public Collider2D Collider { get; protected set; }
    public BaseAIController<BaseObject> AIController { get; protected set; }
    public EObjectState CurrentState { get; set; }
    public int ObjectId { get; set; }
    protected virtual void Awake()
    {
        AnimData = new AnimationData();
    }
    protected virtual void Start() { }
    protected virtual void Update() { }

    public virtual void OnDamage(float damage)
    {
        int retDamage = (int)(damage - StatComponent.GetStat(EStatType.Defence));
        StatComponent.SetStat(EStatType.Hp, StatComponent.GetStat(EStatType.Hp) - retDamage);
        if (StatComponent.GetStat(EStatType.Hp) <= 0)
        {
            OnDie();
        }
    }

    public virtual void OnDie()
    {
    }
}
