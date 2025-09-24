using UnityEngine;

public abstract class State<T>
{
    protected readonly T _controller;

    protected State(T controller)
    {
        _controller = controller;
    }
    
    public abstract void Enter();
    public abstract void Update();
    public abstract void FixedUpdate();
    public abstract void Exit();
}
