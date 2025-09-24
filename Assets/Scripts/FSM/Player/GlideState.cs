using UnityEngine;

public class GlideState : State<PlayerController>
{
    public GlideState(PlayerController controller) : base(controller) { }

    public override void Enter()
    {
        Debug.Log("Enter GlideState");
        _controller.PlayerAnimation.PlayGlide();
        _controller.PlayerHitBox.EnableGlideCollider(true);
    }

    public override void FixedUpdate()
    {
        _controller.PlayerMovement.MoveGlide();
    }

    public override void Update()
    {
        if (!_controller.IsTouching)
            _controller.ChangeState(_controller.PlayerStateFactory.Get<DiveState>());
    }

    public override void Exit()
    {
        _controller.PlayerHitBox.EnableGlideCollider(false);
    }
}
