using UnityEngine;

public class DiveState : State<PlayerController>
{
    public DiveState(PlayerController controller) : base(controller) { }

    public override void Enter()
    {
        Debug.Log("Enter DiveState");
        _controller.PlayerAnimation.PlayDive();
        _controller.PlayerHitBox.EnableDiveCollider(true);
    }

    public override void FixedUpdate()
    {
        _controller.PlayerMovement.MoveDive();
    }

    public override void Update()
    {
        if (_controller.IsTouching)
            _controller.ChangeState(_controller.PlayerStateFactory.Get<GlideState>());
    }

    public override void Exit()
    {
        _controller.PlayerHitBox.EnableDiveCollider(false);
    }
}
