using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator _animator;
    private InputManager _inputManager;
    
    void Start()
    {
        _animator = GetComponent<Animator>();
    }
    
    private void OnEnable()
    {
        if (_inputManager == null)
             _inputManager = InputManager.Instance;
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
        _animator.Play("Glide");
    }

    private void HandlePressCancel()
    {
        _animator.Play("Dive");
    }
}
