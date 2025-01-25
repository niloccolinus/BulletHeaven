using UnityEngine;

public class HitOtherOnCollision : MonoBehaviour
{
    [SerializeField]
    private int _damage;
    [SerializeField]
    private float _pushForce;
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
                // TODO : apply push force
                Health enemyHealth = other.GetComponent<Health>();
                if (enemyHealth != null)
                {
                    enemyHealth.LoseHealth(_damage);
                    // grant rewards if enemy is dead
                    if (enemyHealth.IsDead)
                    {
                        Scoring enemyScoring = other.GetComponent<Scoring>();
                        if (enemyScoring != null)
                        {
                            enemyScoring.GrantRewards();
                        }
                    }
                }
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
