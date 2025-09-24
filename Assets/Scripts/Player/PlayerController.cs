using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerStateFactory PlayerStateFactory { get; private set; }
    public PlayerMovement PlayerMovement { get; private set; }
    public PlayerAnimation PlayerAnimation { get; private set; }
    public PlayerHitBox PlayerHitBox { get; private set; }

    private InputManager _inputManager;
    private PlayerState _currentState;

    
    private void Awake()
    {
        _inputManager = InputManager.Instance;
        PlayerStateFactory = new PlayerStateFactory(this);
        PlayerMovement = GetComponent<PlayerMovement>();
        PlayerAnimation = GetComponent<PlayerAnimation>();
        PlayerHitBox = GetComponent<PlayerHitBox>();
    }

    private void Start()
    {
        ChangeState(PlayerStateFactory.Get<DiveState>());
    }

    private void Update()
    {
        _currentState.Update();
    }

    private void FixedUpdate()
    {
        _currentState.FixedUpdate();
    }

    public void ChangeState(PlayerState newState)
    {
        _currentState?.Exit();
        _currentState = newState;
        _currentState?.Enter();
    }

    private void OnEnable()
    {
        _inputManager.OnStartPressEvent += HandlePressStart;
        _inputManager.OnCancelPressEvent += HandlePressCancel;
    }

    private void OnDisable()
    {
        if (_inputManager == null) return;
        _inputManager.OnStartPressEvent -= HandlePressStart;
        _inputManager.OnCancelPressEvent -= HandlePressCancel;
    }

    private void HandlePressStart(Vector2 input)
    {
        ChangeState(PlayerStateFactory.Get<GlideState>());
    }

    private void HandlePressCancel()
    {
        ChangeState(PlayerStateFactory.Get<DiveState>());
    }
}
