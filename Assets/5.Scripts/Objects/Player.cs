using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enums;

public class Player : Creature
{
    public PlayerData PlayerData { get; private set; }
    private PlayerAIController _aiController;
    [SerializeField]
    GameObject _gunTip;
    protected override void Awake()
    {
        base.Awake();
        Manager.Object.RegisterPlayer(this);
        SkillComponent = new SkillComponent(this);
        StatComponent = new StatComponent(this);
        _aiController = new PlayerAIController(this);
        PlayerData = new PlayerData()
        {
            skillTemplateId = 1,
            attackDamage = 40,
            attackSpeed = 1,
            defence = 1,
            maxHp = 100,
            playerType = EPlayerType.Normal,
            prefabName = "Player",
        };

        SetInfo();
    }

    protected override void Update()
    {
        _aiController.Update();
    }

    public Vector2 GetGunTipPos()
    {
        return _gunTip.transform.position;
    }

    private void SetInfo()
    {
        SetStatInfo(new StatInfo()
        {
            MaxHp = PlayerData.maxHp,
            Hp = PlayerData.maxHp,
            AttackDamage = PlayerData.attackDamage,
            Defence = PlayerData.defence,
            AttackSpeed = PlayerData.attackSpeed,
        });
        SkillData skillData = new SkillData()
        {
            animName = "NormalAttack",
            animTime = 1,
            animParamName = "IsAttack",
            castingTime = 0.6f,
            maxHitCount = 3,
            sectorAngle = 30,
            skillAreaType = ESkillAreaType.Corn,
            skillRange = 10,
            templateId = 1,
        };
        SkillComponent.RegisterSkill(skillData);
    }
}
