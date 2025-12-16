using UnityEngine;

public class PlayerMove : PlayerState
{
    public PlayerMove(Player player, StateMachine sm) : base(player, sm) { }

    public override void Enter()
    {
        if (player.bodyAnimator != null)
            player.bodyAnimator.SetBool(Animator.StringToHash("IsMoving"), true);
    }

    public override void Tick()
    {
        if (player.GetAttackInput().sqrMagnitude > 0.01f)
        {
            stateMachine.ChangeState<PlayerAttack>();
            return;
        }

        if (player.GetMoveInput().sqrMagnitude < 0.01f)
            stateMachine.ChangeState<PlayerIdle>();
    }

    public override void FixedTick()
    {
        var input = player.GetMoveInput().normalized;
        player.SetVelocity(input * player.moveSpeed);
    }
}