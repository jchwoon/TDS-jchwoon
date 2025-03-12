using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using static Enums;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class SkillComponent
{
    public Creature Owner { get; private set; }
    public bool IsSkillUsing { get; private set; }
    public SkillData SkillData { get; private set; }

    public SkillComponent(Creature owner)
    {
        Owner = owner;
    }

    public void RegisterSkill(int skillTemplateId)
    {
        if (Manager.Data.SkillDict.TryGetValue(skillTemplateId, out SkillData data) == false)
            return;

        SkillData = data;
    }

    public void RegisterSkill(SkillData data)
    {
        SkillData = data;
    }

    public void CheckAndUseSkill(BaseObject target)
    {
        if (CheckCanUseSkill() == false)
            return;

        if (Owner.Animator != null)
        {
            Owner.Animator.Play(Owner.AnimData.SkillHash);
        }

        CoroutineHelper.Instance.StartCoroutine(CoCastingTime(target));
        CoroutineHelper.Instance.StartCoroutine(CoAnimTime());
    }

    private bool CheckCanUseSkill()
    {
        if (IsSkillUsing)
            return false;

        return true;
    }

    //스킬 시전 시간
    private IEnumerator CoCastingTime(BaseObject target)
    {
        yield return new WaitForSeconds(GetCalculateTimeByAttackSpeed(SkillData.castingTime));
        UseSkill(target);
    }
    //스킬 애니메이션 시간
    private IEnumerator CoAnimTime()
    {
        IsSkillUsing = true;
        yield return new WaitForSeconds(GetCalculateTimeByAttackSpeed(SkillData.animTime));
        IsSkillUsing = false;
    }

    //공격속도에 비례한 캐스팅 시간 또는 애니메이션 시간 계산
    private float GetCalculateTimeByAttackSpeed(float time)
    {
        float attackSpeed = Owner.StatComponent.GetStat(EStatType.AttackSpeed);
        return time / attackSpeed;
    }

    private void UseSkill(BaseObject target)
    {
        if (SkillData.skillAreaType == ESkillAreaType.None)
        {
            target.OnDamage(Owner.StatComponent.GetStat(EStatType.AttackDamage));
            return;
        }

        if (SkillData.skillAreaType == ESkillAreaType.Corn)
        {
            Player p = Owner as Player;
            Vector2 skillDir = (GetMouseWorldPos() - p.GetGunTipPos()).normalized;
            float skillDist = SkillData.skillRange;

            int mask = (1 << (int)ELayermask.Top) | (1 << (int)ELayermask.Bottom) | (1 << (int)ELayermask.Middle);
            Collider2D[] colliders = Physics2D.OverlapCircleAll(Owner.transform.position, skillDist, mask);
            List<BaseObject> closestObjects = new List<BaseObject>();
            //스킬 범위 안 목표물 등록
            for (int i = 0; i < colliders.Length; i++)
            {
                BaseObject bo = colliders[i].GetComponent<BaseObject>();
                if (bo == null)
                    continue;

                Vector2 targetDir = (bo.gameObject.transform.position - Owner.transform.position).normalized;

                float dotValue = Vector2.Dot(targetDir, skillDir);
                float skillSectorValue = MathF.Cos((SkillData.sectorAngle / 2) * Mathf.Deg2Rad);
                if (dotValue >= skillSectorValue)
                {
                    closestObjects.Add(bo);
                }
            }
            //가장 가까운 maxHitCount명 뽑기위한 정렬
            closestObjects.Sort((a, b) =>
            {
                float distanceA = (Owner.transform.position - a.transform.position).sqrMagnitude;
                float distanceB = (Owner.transform.position - b.transform.position).sqrMagnitude;
                return distanceA.CompareTo(distanceB);
            });

            //데미지 입히기
            int hitCount = 0;
            for (int i = 0; i < closestObjects.Count && hitCount < SkillData.maxHitCount; i++)
            {
                closestObjects[i].OnDamage(Owner.StatComponent.GetStat(EStatType.AttackDamage));
                hitCount++;
            }

            Debug.DrawRay(p.GetGunTipPos(), skillDir * skillDist, Color.red, 1.0f);

            return;
        }
    }

    private Vector2 GetMouseWorldPos()
    {
        Vector2 mouseScreenPosition = Input.mousePosition;

        return Camera.main.ScreenToWorldPoint(mouseScreenPosition);
    }
}
