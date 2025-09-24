using UnityEngine;

public class PlayerAttackCollider : MonoBehaviour
{
    [SerializeField] private int damage = 1;

    private void OnCollisionEnter2D(Collision2D other)
    {
        EnemyBase enemy = other.gameObject.GetComponent<EnemyBase>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Debug.Log($"{enemy.name} 적이 피격됨!");
        }
    }
}
