using System.Collections;
using UnityEngine;

public class HitOtherOnCollision : MonoBehaviour
{
    [SerializeField]
    private int _damage;
    [SerializeField]
    private float _pushForce = 5f;
    [SerializeField]
    private float _pushDuration = 0.2f;
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
                // push enemy
                Rigidbody enemyRigidbody = other.GetComponent<Rigidbody>();
                // pushDirection calculated by substracting projectile position by enemy position
                Vector3 pushDirection = (other.transform.position - transform.position).normalized;
                StartCoroutine(ApplyPushForce(enemyRigidbody, pushDirection));

                // hurt enemy
                Health enemyHealth = other.GetComponent<Health>();
                enemyHealth.LoseHealth(_damage);

                // grant rewards if enemy is dead
                if (enemyHealth.IsDead)
                {
                    Scoring enemyScoring = other.GetComponent<Scoring>();
                    enemyScoring.GrantRewards();
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
    private IEnumerator ApplyPushForce(Rigidbody enemyRigidbody, Vector3 pushDirection)
    {
        // disable enemy controller
        EnemyController enemyController = enemyRigidbody.GetComponent<EnemyController>();
        enemyController.enabled = false;

        // apply force
        enemyRigidbody.AddForce(pushDirection * _pushForce, ForceMode.Impulse);

        yield return new WaitForSeconds(_pushDuration);

        // reduce rigidbody speed to stop the movement
        enemyRigidbody.linearVelocity = Vector3.zero;

        // re-enable enemy controller
        enemyController.enabled = true;
    }
}
