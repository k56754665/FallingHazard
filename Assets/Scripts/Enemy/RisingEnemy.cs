using System;
using UnityEngine;

public class RisingEnemy : EnemyBase
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private int damage = 1;

    protected override void Start()
    {
        base.Start();
        Damage = damage;
    }
    
    private void FixedUpdate()
    {
        rb.linearVelocity = Vector2.up * (speed * SpeedSystem.Speed);
    }
}
