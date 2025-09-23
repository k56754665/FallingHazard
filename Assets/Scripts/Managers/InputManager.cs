using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    private InputSystemActions _actions;
    
    public event Action<Vector2> OnStartPressEvent;
    public event Action OnCancelPressEvent;
    public event Action<Vector2> OnPositionEvent;
    public Vector2 MoveVector { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        
        _actions = new InputSystemActions();
    }

    private void OnEnable()
    {
        _actions.Enable();
        _actions.Player.Press.started += OnPressStarted;
        _actions.Player.Press.canceled += OnPressCanceled;
        _actions.Player.Position.performed += OnPosition;
    }

    private void OnDisable()
    {
        _actions.Player.Press.started -= OnPressStarted;
        _actions.Player.Press.canceled -= OnPressCanceled;
        _actions.Player.Position.performed -= OnPosition;
        _actions.Disable();
    }

    private void OnPressStarted(InputAction.CallbackContext ctx)
    {
        Vector2 screenPos = Pointer.current.position.ReadValue();
        OnStartPressEvent?.Invoke(screenPos);
    }

    private void OnPressCanceled(InputAction.CallbackContext ctx)
    {
        OnCancelPressEvent?.Invoke();
    }

    private void OnPosition(InputAction.CallbackContext ctx)
    {
        Vector2 screenPos = ctx.ReadValue<Vector2>();
        OnPositionEvent?.Invoke(screenPos);
    }
    
    public void SetMoveVector(Vector2 moveVector)
    {
        MoveVector = moveVector;
    }
}