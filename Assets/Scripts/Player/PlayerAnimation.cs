using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private static readonly int IsTouching = Animator.StringToHash("IsTouching");
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayGlide() => _animator.SetBool(IsTouching, true);
    public void PlayDive() => _animator.SetBool(IsTouching, false);
}
