using UnityEngine;


public class JumpState : State
{
    protected internal override void OnEnter(StateController stateController)
    {
        base.OnEnter(stateController);
        stateController.Owner.Animator.CrossFade("Jump", 0.1f);
    }

    protected internal override void OnUpdate(float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(elapseSeconds, realElapseSeconds);
        AnimatorStateInfo animatorStateInfo = m_StateController.Owner.Animator.GetCurrentAnimatorStateInfo(0);
        bool playingJump = animatorStateInfo.IsName("Jump");
        if (playingJump && animatorStateInfo.normalizedTime >= 1)
        {
            m_StateController.OnChangeState(m_StateController.Owner.IdleState);
        }
    }

    protected internal override void OnLeave()
    {
        base.OnLeave();

    }
}
