using System;
using System.Collections.Generic;


public class StateMachine
{
    private IState _currentState;
    private Dictionary<Type, IState> _states = new Dictionary<Type, IState>();

    public void AddState(IState state)
    {
        _states[state.GetType()] = state;
    }

    public void ChangeState<T>() where T : IState
    {
        var type = typeof(T);
        if (_currentState != null && _currentState.GetType() == type)
            return;

        _currentState?.Exit();

        if (_states.TryGetValue(type, out var newState))
        {
            _currentState = newState;
            _currentState.Enter();
        }
    }

    public void Tick() => _currentState?.Tick();
    public void FixedTick() => _currentState?.FixedTick();
}
