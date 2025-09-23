using System;
using Unity.Cinemachine;
using UnityEngine;

public class CameraImpulseGenerator : MonoBehaviour
{
    [SerializeField] private CinemachineImpulseSource _bump;
    private InputManager _inputManager;

    void Start()
    {
        _inputManager = InputManager.Instance;
        _inputManager.OnStartPressEvent += Bump;
    }

    private void OnDestroy()
    {
        _inputManager.OnStartPressEvent -= Bump;
    }

    private void Bump(Vector2 input)
    {
        _bump.GenerateImpulseWithForce(SpeedSystem.Speed);
    }
}
