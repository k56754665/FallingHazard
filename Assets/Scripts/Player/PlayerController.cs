using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public StateFactory<PlayerController> PlayerStateFactory { get; private set; }
    public PlayerMovement PlayerMovement { get; private set; }
    public PlayerAnimation PlayerAnimation { get; private set; }
    public PlayerHitBox PlayerHitBox { get; private set; }
    public bool IsTouching { get; private set; }

    private InputManager _inputManager;
    private State<PlayerController> _currentState;
    private int _hp = 5;
    
    private void Awake()
    {
        _inputManager = InputManager.Instance;
        PlayerStateFactory = new StateFactory<PlayerController>(this);
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

    public void ChangeState(State<PlayerController> newState)
    {
        _currentState?.Exit();
        _currentState = newState;
        _currentState?.Enter();
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        EnemyBase enemy = other.gameObject.GetComponent<EnemyBase>();
        if (enemy != null)
        {
            TakeDamage(enemy.Damage);
        }
    }

    private void TakeDamage(int damage)
    {
        _hp -= Mathf.Max(damage, 0);
        
        if (_hp <= 0)
            Die();
    }

    private void Die()
    {
        Destroy(gameObject);
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
        IsTouching = true;
    }

    private void HandlePressCancel()
    {
        IsTouching = false;
    }
}
