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
        Destroy(gameObject);
    }
}
