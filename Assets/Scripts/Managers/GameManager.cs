using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public float CurrentSpeed { get; private set; }
    
    public void SetSpeed(float newSpeed)
    {
        CurrentSpeed = newSpeed;
    }
}
