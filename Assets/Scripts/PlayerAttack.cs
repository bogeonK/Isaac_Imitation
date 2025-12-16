using UnityEngine;

public class PlayerAttack : PlayerState
{
    public PlayerAttack(Player player, StateMachine sm) : base(player, sm) { }

    public override void Enter()
    {
        if (player.bodyAnimator != null)
            player.bodyAnimator.SetTrigger("Attack");

        FireTear(); 
    }

    public override void Tick()
    {
        var atk = player.GetAttackInput();
        if (atk.sqrMagnitude > 0.01f)
        {
            FireTear(); 
            return;
        }

        var move = player.GetMoveInput();
        if (move.sqrMagnitude > 0.01f) stateMachine.ChangeState<PlayerMove>();
        else stateMachine.ChangeState<PlayerIdle>();
    }

    void FireTear()
    {
        // 헤드 깜빡임은 발사 성공했을 때만
        Vector2 dir = player.GetAttackInput().normalized;

        if (player.tearShooter != null && player.tearShooter.TryFire(dir))
        {
            player.headVisual?.TearOnce(0.08f); 
        }
    }
}
