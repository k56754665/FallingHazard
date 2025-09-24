using UnityEngine;

public abstract class PlayerState
{
    protected readonly PlayerController _player;
    
    public PlayerState(PlayerController player)
    {
        _player = player;
    }

    public abstract void Enter();
    public abstract void FixedUpdate();
    public abstract void Update();
    public abstract void Exit();

    protected void DebugState(string msg)
    {
        Debug.Log($"[{GetType().Name}] {msg}");
    }
}
