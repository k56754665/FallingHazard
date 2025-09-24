using UnityEngine;

public class DiveState : PlayerState
{
    public DiveState(PlayerController player) : base(player) { }

    public override void Enter()
    {
        Debug.Log("Enter DiveState");
        _player.PlayerAnimation.PlayDive();
        _player.PlayerHitBox.EnableDiveCollider(true);
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
        _player.PlayerHitBox.EnableDiveCollider(false);
    }
}
