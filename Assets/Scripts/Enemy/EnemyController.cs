using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private float _speed = 0.5f;
    
    void Update()
    {
        transform.position += Vector3.up * (Time.deltaTime * _speed * SpeedSystem.Speed);
    }
}
