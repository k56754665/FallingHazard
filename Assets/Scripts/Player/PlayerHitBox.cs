using UnityEngine;

public class PlayerHitBox : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _diveCollider;
    [SerializeField] private BoxCollider2D _glideCollider;

    public void EnableGlideCollider(bool isEnable)
    {
        _glideCollider.enabled = isEnable;
    }

    public void EnableDiveCollider(bool isEnable)
    {
        _diveCollider.enabled = isEnable;
    }
}
