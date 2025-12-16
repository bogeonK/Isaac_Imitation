using UnityEngine;

public class PlayerIdle : PlayerState
{
    public PlayerIdle(Player player, StateMachine sm) : base(player, sm) { }

    public override void Enter()
    {
        if (player.bodyAnimator != null)
            player.bodyAnimator.SetBool(Animator.StringToHash("IsMoving"), false);
    }

    public override void Tick()
    {
        if (player.GetAttackInput().sqrMagnitude > 0.01f)
        {
            stateMachine.ChangeState<PlayerAttack>();
            return;
        }

        if (player.GetMoveInput().sqrMagnitude > 0.01f)
            stateMachine.ChangeState<PlayerMove>();
    }

    public override void FixedTick()
    {
        player.SetVelocity(Vector2.zero);
    }
}
