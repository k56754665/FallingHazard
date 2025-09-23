using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float fallAccel = 20f;
    [SerializeField] private float maxFallSpeed = 10f;
    [SerializeField] private float climbSpeed = 5f;

    private Rigidbody2D _rb;
    private bool _isPressing;
    private InputManager _inputManager;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0f; // 직접 중력 구현
        _inputManager = InputManager.Instance;
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

    private void FixedUpdate()
    {
        Vector2 input = InputManager.Instance.MoveVector;

        // --- 아래 방향 입력은 무시 ---
        if (input.y < 0)
            input.y = 0;

        // --- 좌우 이동 ---
        float targetXVel = input.x * moveSpeed;

        // --- Y축 처리 ---
        float targetYVel = 0f;

        if (!_isPressing) 
        {
            // 입력이 없으면 낙하
            _rb.AddForce(Vector2.down * fallAccel, ForceMode2D.Force);

            // 최대 낙하 속도 제한
            if (_rb.linearVelocity.y < -maxFallSpeed)
                _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, -maxFallSpeed);

            targetYVel = _rb.linearVelocity.y;
        }
        else
        {
            // 위쪽 입력이 있을 때만 오름
            targetYVel = input.y * climbSpeed;
        }

        // 최종 속도 적용
        _rb.linearVelocity = new Vector2(targetXVel, targetYVel);
    }

    private void HandlePressStart(Vector2 input)
    {
        _isPressing = true;
    }

    private void HandlePressCancel()
    {
        _isPressing = false;
    }
}