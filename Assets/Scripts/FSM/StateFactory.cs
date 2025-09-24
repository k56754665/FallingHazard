using System;
using System.Collections.Generic;
using UnityEngine;

public class StateFactory<TController>
{
    private readonly TController _controller;
    private readonly Dictionary<Type, State<TController>> _states = new();

    public StateFactory(TController controller)
    {
        _controller = controller;
    }

    public TState Get<TState>() where TState : State<TController>
    {
        var type = typeof(TState);

        if (_states.TryGetValue(type, out var state))
            return (TState)state;
        
        var newState = (TState)Activator.CreateInstance(typeof(TState), _controller);
        _states[type] = newState;
        return newState;
    }
}
