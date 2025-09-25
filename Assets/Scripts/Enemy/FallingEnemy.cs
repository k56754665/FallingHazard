using UnityEngine;

public class FallingEnemy : EnemyBase
{
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    [SerializeField] private int minSpeed;

    protected override void Start()
    {
        base.Start();
        Damage = damage;
    }
    
    private void Update()
    {
        // 화면 아래로 나가면 삭제
        if (transform.position.y < -8f)
        {
            Destroy(gameObject);
        }
    }
    
    private void FixedUpdate()
    {
        rb.linearVelocity = Vector2.down * Mathf.Max((speed - SpeedSystem.Speed), minSpeed);
    }
}
