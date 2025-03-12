using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Define;
using static Enums;

public class MonsterAIController : CreatureAIController<Monster, BaseObject>
{
    private float _collisionCheckDistance = 0.3f;
    private float _backPushForce = 4f;

    public MonsterAIController(Monster owner) : base(owner)
    {
    }

    public override void FindTarget()
    {
        List<BaseObject> objects = new List<BaseObject>();

        objects.AddRange(Manager.Object.GetAllBoxes());
        objects.Add(Manager.Object.Player);

        float minDistance = float.MaxValue;

        foreach (BaseObject obj in objects)
        {
            if (obj == null || obj.CurrentState == EObjectState.Die)
                continue;
            float sqrDistance = (Owner.transform.position - obj.transform.position).sqrMagnitude;
            if (sqrDistance < minDistance)
            {
                minDistance = sqrDistance;
                Target = obj as BaseObject;
            }
        }
    }

    public override void Update()
    {
        FindTarget();
        base.Update();
    }

    protected override void UpdateMove()
    {
        base.UpdateMove();


    }

    protected override void FixedUpdateMove()
    {
        base.FixedUpdateMove();
        if (!IsValidTarget())
        {
            return;
        }

        if (CheckCanAttack())
        {
            ChangeState(EObjectState.Skill);
            return;
        }

        if (IsGrounded())
        {
            int layerMask = 1 << Owner.gameObject.layer;
            RaycastHit2D hit = Physics2D.Raycast(GetBoundsEdge(Owner.Collider, EBoundsEdge.Left), Vector2.left, _collisionCheckDistance, layerMask);

            if (hit.collider && hit.collider.gameObject != Owner.gameObject)
            {
                //앞의 물체 위에 물체가 없으면 점프
                if (IsObstructedAbove(hit.collider) == false)
                {
                    Jump();
                }
            }
            else
            {
                MoveTowardsTarget();
            }
        }
        else
        {
            MoveTowardsTarget();
        }
    }

    protected override void UpdateSkill()
    {
        base.UpdateSkill();
        if (Owner.SkillComponent.IsSkillUsing)
            return;

        if (!CheckCanAttack())
        {
            ChangeState(EObjectState.Move);
            return;
        }

        //착지 상태이면서 위에 물체가 있으면 뒤로 밀리기
        if (IsGrounded() && IsObstructedAbove(Owner.Collider))
        {
            BackToMove();
        }

        Owner.SkillComponent.CheckAndUseSkill(Target);
    }

    private bool CheckCanAttack()
    {
        int mask = 1 << (int)ELayermask.Truck;
        //float attackRange = Owner.StatComponent.GetStat(EStatType.AttackRange);
        RaycastHit2D hit = Physics2D.Raycast(GetBoundsEdge(Owner.Collider, EBoundsEdge.Left), Vector2.left, 0f,mask);
        return hit.collider != null;
    }

    //발이 바닥에 있는지
    private bool IsGrounded()
    {
        int mask = 1 << Owner.gameObject.layer;
        RaycastHit2D hit = Physics2D.Raycast(GetBoundsEdge(Owner.Collider, EBoundsEdge.Bottom), Vector2.down, _collisionCheckDistance, mask);
        return hit.collider != null;
    }

    //checkCollider 위에 물체가 있는지
    private bool IsObstructedAbove(Collider2D checkCollider)
    {
        int mask = 1 << checkCollider.gameObject.layer;
        RaycastHit2D hit = Physics2D.Raycast(GetBoundsEdge(checkCollider, EBoundsEdge.Top), Vector2.up, _collisionCheckDistance, mask);
        return hit.collider != null;
    }

    private void BackToMove()
    {
        int mask = 1 << Owner.gameObject.layer;
        Owner.Rigidbody.velocity = new Vector2(_backPushForce, Owner.Rigidbody.velocity.y);

        int count = 0;
        while (true)
        {
            if (count++ > 10)
                break;

            RaycastHit2D hit = Physics2D.Raycast(GetBoundsEdge(Owner.Collider, EBoundsEdge.Right), Vector2.right, 1f, mask);
            if (hit.collider == null)
                break;

            BaseObject bo = hit.collider.GetComponent<BaseObject>();
            bo.Rigidbody.velocity = new Vector2(_backPushForce, bo.Rigidbody.velocity.y);
        }
    }

    private void MoveTowardsTarget()
    {
        Vector2 direction = GetDirectionToTarget();
        Owner.Rigidbody.velocity = new Vector2(direction.x * Owner.StatComponent.GetStat(EStatType.MoveSpeed), Owner.Rigidbody.velocity.y);
    }

    private Vector2 GetDirectionToTarget()
    {
        if (IsValidTarget() == false)
        {
            return Vector2.zero;
        }

        return (Target.transform.position - Owner.transform.position).normalized;
    }

    private void Jump()
    {
        Owner.Rigidbody.velocity = new Vector2(Owner.Rigidbody.velocity.x, Owner.StatComponent.GetStat(EStatType.MoveSpeed));
    }

    public Vector2 GetBoundsEdge(Collider2D collider, EBoundsEdge edge)
    {
        Vector2 center = collider.bounds.center;
        Vector2 extents = collider.bounds.extents;

        switch (edge)
        {
            case EBoundsEdge.Top:
                return new Vector2(center.x, center.y + extents.y + 0.1f);
            case EBoundsEdge.Bottom:
                return new Vector2(center.x, center.y - extents.y - 0.1f);
            case EBoundsEdge.Left:
                return new Vector2(center.x - extents.x - 0.1f, center.y);
            case EBoundsEdge.Right:
                return new Vector2(center.x + extents.x + 0.1f, center.y);
            default:
                return center;
        }
    }

}