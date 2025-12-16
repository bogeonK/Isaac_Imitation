using UnityEngine;

public class PlayerState : IState
{
    protected Player player;
    protected StateMachine stateMachine;

    protected PlayerState(Player player, StateMachine stateMachine)
    {
        this.player = player;
        this.stateMachine = stateMachine;
    }

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Tick() { }
    public virtual void FixedTick() { }
}
