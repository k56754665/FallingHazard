using UnityEngine;

public class GlideState : PlayerState
{
    public GlideState(PlayerController player) : base(player) { }

    public override void Enter()
    {
        Debug.Log("Enter GlideState");
        _player.PlayerAnimation.PlayGlide();
        _player.PlayerHitBox.EnableGlideCollider(true);
    }

    public override void FixedUpdate()
    {
        _player.PlayerMovement.MoveGlide();
    }

    public override void Update()
    {
        
    }

    public override void Exit()
    {
        _player.PlayerHitBox.EnableGlideCollider(false);
    }
}
