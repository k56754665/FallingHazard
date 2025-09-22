using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public float CurrentSpeed { get; private set; }

    private void Start()
    {
        CurrentSpeed = 1f;
    }

    public void SetSpeed(float newSpeed)
    {
        CurrentSpeed = newSpeed;
    }
}
