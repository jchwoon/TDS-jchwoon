using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObjectState
{
    void Enter();
    void Update();
    void Exit();
}

public class BaseState<TOwner> : IObjectState where TOwner : BaseObject
{
    protected BaseAIController<TOwner> _baseAIController;
    protected TOwner _owner;
    public BaseState(BaseAIController<TOwner> baseAIController)
    {
        _baseAIController = baseAIController;
        _owner = baseAIController.Owner;
    }
    public virtual void Enter()
    {
    }
    public virtual void Update()
    {
    }
    public virtual void Exit()
    {
    }
}

#region IdleState
public class IdleState<TOwner> : BaseState<TOwner>, IObjectState where TOwner : BaseObject
{
    public IdleState(BaseAIController<TOwner> baseAIController) : base(baseAIController)
    {
    }

    public override void Enter()
    {
    }
    public override void Update()
    {
        Debug.Log("IdleState"); 
        //_baseAIController.target
    }
    public override void Exit()
    {
    }
}
#endregion

#region MoveState
public class MoveState<TOwner> : BaseState<TOwner>, IObjectState where TOwner : BaseObject
{
    public MoveState(BaseAIController<TOwner> baseAIController) : base(baseAIController)
    {
    }

    public override void Enter()
    {
    }
    public override void Update()
    {
    }
    public override void Exit()
    {
    }
}
#endregion

#region SkillState
public class SkillState<TOwner> : BaseState<TOwner>, IObjectState where TOwner : BaseObject
{
    public SkillState(BaseAIController<TOwner> baseAIController) : base(baseAIController)
    {
    }

    public override void Enter()
    {
    }
    public override void Update()
    {
    }
    public override void Exit()
    {
    }
}
#endregion