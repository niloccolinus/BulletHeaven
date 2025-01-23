using UnityEngine;

public class HitOtherOnCollision : MonoBehaviour
{
    [SerializeField]
    private int _damage;
    [SerializeField]
    private bool _destroyItself = false;
    [SerializeField]
    private bool _isEnemy = true;

    private void OnTriggerEnter(Collider other)
    {
        if (!_isEnemy)
        {
            // if projectile or weapon
            if (other.CompareTag("Enemy"))
            {
                other.GetComponent<Health>().LoseHealth(_damage);
                if (_destroyItself) Destroy(gameObject);
            }
        }
        else
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<Health>().LoseHealth(_damage);
            }
        }

    }
}
