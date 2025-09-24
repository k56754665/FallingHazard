using UnityEngine;

public class DiveState : PlayerState
{
    public DiveState(PlayerController player) : base(player) { }

    public override void Enter()
    {
        Debug.Log("Enter DiveState");
        _player.PlayerAnimation.PlayDive();
    }

    public override void FixedUpdate()
    {
        _player.PlayerMovement.MoveDive();
    }

    public override void Update()
    {
        
    }

    public override void Exit()
    {
        
    }
}
