using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected int hp = 1;
    public int Damage { get; protected set; }

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        // 화면 위로 나가면 삭제
        if (transform.position.y > 8f)
        {
            Destroy(gameObject);
        }
    }
    
    public void TakeDamage(int dmg)
    {
        hp -= dmg;
        if (hp <= 0)
        {
            Die();
        }
    }
    
    private void Die()
    {
        Debug.Log($"{name} 사망!");
        Destroy(gameObject);
    }
}
