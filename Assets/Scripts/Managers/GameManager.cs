using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public float Speed { get; private set; }
    
    [SerializeField] private float _diveSpeed;
    [SerializeField] private float _glideSpeed;
    [SerializeField] private float _accel;
    private float _targetSpeed;

    private void Start()
    {
        _targetSpeed = _glideSpeed;
    }

    private void Update()
    {
        Speed = Mathf.Lerp(Speed, _targetSpeed, Time.deltaTime * _accel);
        Debug.Log($"Speed: {Speed}, Target: {_targetSpeed}");

    }

    public void SetDive(bool isDive)
    {
        _targetSpeed = isDive ? _diveSpeed : _glideSpeed;
    }
}
