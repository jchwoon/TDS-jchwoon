using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;
using static Enums;

public class Monster : Creature
{
    public MonsterData MonsterData { get; private set; }

    private MonsterAIController _aiController;
    private DamageFontController _damageFontController;

    protected override void Awake()
    {
        base.Awake();
        Animator = GetComponent<Animator>();
        Rigidbody = GetComponent<Rigidbody2D>();
        Collider = GetComponent<Collider2D>();
        _damageFontController = GetComponent<DamageFontController>();
    }
    public void Init(MonsterData data)
    {
        MonsterData = data;
        _aiController = new MonsterAIController(this);
        SkillComponent = new SkillComponent(this);
        StatComponent = new StatComponent(this);

        SetInfo();
    }

    public void SetPos(Vector2 pos)
    {
        transform.position = pos;
    }

    protected override void Update()
    {
        base.Update();
        if(_aiController != null)
        {
            _aiController.Update();
        }
    }
   
    private void FixedUpdate()
    {
        if (_aiController != null)
        {
            _aiController.FixedUpdate();
        }
    }

    public override void OnDamage(float damage)
    {
        base.OnDamage(damage);
        _damageFontController.RegisterOrSpawnText(damage, transform);
    }

    public override void OnDie()
    {
        StatComponent.SetStat(EStatType.Hp, StatComponent.GetStat(EStatType.MaxHp));
        _aiController.ChangeState(EObjectState.Die);
        Manager.Object.Despawn(ObjectId);
    }

    private void SetInfo()
    {
        SetSkillInfo(MonsterData.skillTemplateId);
        SetStatInfo(new StatInfo()
        {
            MaxHp = MonsterData.maxHp,
            Hp = MonsterData.maxHp,
            AttackDamage = MonsterData.attackDamage,
            Defence = MonsterData.defence,
            MoveSpeed = MonsterData.moveSpeed,
            AttackSpeed = MonsterData.attackSpeed,
            AttackRange = MonsterData.attackRange
        });

        //레이어 & 정렬순서 설정
        int layer = UnityEngine.Random.Range((int)ELayermask.Bottom, (int)ELayermask.Top + 1);
        string sortingLayer = "";
        switch (layer)
        {
            case (int)ELayermask.Bottom:
                sortingLayer = SortingLayers.Bottom;
                break;
            case (int)ELayermask.Middle:
                sortingLayer = SortingLayers.Middle;
                break;
            case (int)ELayermask.Top:
                sortingLayer = SortingLayers.Top;
                break;
        }

        SetLayerMask(gameObject, layer);
        SetSortingLayer(gameObject, sortingLayer);
    }

    private void SetSortingLayer(GameObject go, string sortingLayer)
    {
        SpriteRenderer[] spriteRenderers = go.GetComponentsInChildren<SpriteRenderer>();

        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            spriteRenderer.sortingLayerName = sortingLayer;
        }
    }

    private void SetLayerMask(GameObject go, int layer)
    {
        go.layer = layer;
    }
}
