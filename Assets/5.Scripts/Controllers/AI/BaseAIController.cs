using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enums;

public interface IAIController
{
    public void Update();
}

public class BaseAIController<TOwner> : IAIController where TOwner : BaseObject
{
    public TOwner Owner { get; protected set; }
    public EObjectState CurrentState
    {
        get { return Owner.CurrentState; }
        set { Owner.CurrentState = value; }
    }
    public BaseAIController(TOwner owner)
    {
        Owner = owner;
        ChangeState(EObjectState.Move);
    }

    public void ChangeState(EObjectState state)
    {
        if (CurrentState == state)
            return;

        CurrentState = state;
        ChangeAnimation();
    }

    public virtual void Update()
    {
        switch (CurrentState)
        {
            case EObjectState.Move:
                UpdateMove();
                break;
            case EObjectState.Skill:
                UpdateSkill();
                break;
        }
    }

    public virtual void FixedUpdate()
    {
        switch (CurrentState)
        {
            case EObjectState.Move:
                FixedUpdateMove();
                break;
        }
    }

    public void ChangeAnimation()
    {
        switch (CurrentState)
        {
            case EObjectState.Move:
                PlayAnimation(Owner.AnimData.MoveHash);
                break;
        }
    }

    protected virtual void UpdateMove()
    {
    }

    protected virtual void UpdateSkill()
    {
    }

    protected virtual void FixedUpdateMove()
    {
    }

    private void PlayAnimation(int animHash)
    {
        Owner.Animator.Play(animHash);
    }
}