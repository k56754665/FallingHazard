using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("이동 속도")]
    [SerializeField] private float moveSpeed;

    [Header("Glide")]
    [SerializeField] private float climbSpeed;   // 기본 상승 속도
    [SerializeField] private float climbBoost;   // 입력에 따른 추가 상승 속도

    [Header("Dive")]
    [SerializeField] private float fallAccel;   // 낙하 가속도
    [SerializeField] private float maxFallSpeed; // 최대 낙하 속도

    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0f; // 직접 중력 구현
    }

    private Vector2 GetInput()
    {
        return InputManager.Instance.MoveVector;
    }

    /// <summary>
    /// Glide 상태 이동
    /// </summary>
    public void MoveGlide()
    {
        Vector2 input = GetInput();

        float targetXVel = input.x * moveSpeed;
        float targetYVel = Mathf.Max(climbSpeed + input.y * climbBoost, climbSpeed);

        _rb.linearVelocity = new Vector2(targetXVel, targetYVel);
    }


    /// <summary>
    /// Dive 상태 이동
    /// </summary>
    public void MoveDive()
    {
        Vector2 input = GetInput();
        
        _rb.AddForce(Vector2.down * fallAccel, ForceMode2D.Force);
        
        var vel = _rb.linearVelocity;
        
        if (vel.y < -maxFallSpeed) vel.y = -maxFallSpeed;

        float targetXVel = input.x * moveSpeed;

        _rb.linearVelocity = new Vector2(targetXVel, vel.y);
    }
}