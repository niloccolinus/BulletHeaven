using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class HitOtherOnCollision : MonoBehaviour
{
    [Header("Damage and Push Settings")]
    [SerializeField]
    private int _damage;
    [SerializeField]
    private float _pushForce = 5f;
    [SerializeField]
    private float _pushDuration = 0.2f;

    [Header("Behavior Settings")]
    [SerializeField]
    private bool _destroyItself = false;
    [SerializeField]
    private bool _isEnemy = true;
    [SerializeField]
    private float _damageInterval = 1f;

    [Header("Visual Effects")]
    [SerializeField]
    private VisualEffect _impactEffect;

    private float _timeSinceLastDamage = 0f;

    private void OnCollisionEnter(Collision collision)
    {
        if (!_isEnemy && collision.collider.CompareTag("Enemy"))
        {
            HandleEnemyCollision(collision);
        }
        else if (_isEnemy && collision.collider.CompareTag("Player"))
        {
            ApplyDamageToPlayer(collision);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (_isEnemy && collision.collider.CompareTag("Player"))
        {
            _timeSinceLastDamage += Time.deltaTime;

            // apply damage if enough time has passed
            if (_timeSinceLastDamage >= _damageInterval)
            {
                ApplyDamageToPlayer(collision);
                _timeSinceLastDamage = 0f; // reset timer
            }
        }
    }

    private void HandleEnemyCollision(Collision collision)
    {
        // get impact position
        ContactPoint contact = collision.GetContact(0);
        Vector3 impactPosition = contact.point;

        // play VFX at the impact position
        Instantiate(_impactEffect, impactPosition, Quaternion.identity);

        // apply push to the enemy
        Rigidbody enemyRigidbody = collision.collider.GetComponent<Rigidbody>();
        Vector3 pushDirection = (collision.transform.position - transform.position).normalized;
        StartCoroutine(ApplyPushForce(enemyRigidbody, pushDirection));


        // deal damage to the enemy
        Health enemyHealth = collision.collider.GetComponent<Health>();
        enemyHealth.LoseHealth(_damage);

        // grant rewards if the enemy dies
        if (enemyHealth.IsDead)
        {
            Scoring enemyScoring = collision.collider.GetComponent<Scoring>();
            enemyScoring?.GrantRewards();
        }

        // destroy the projectile if required
        if (_destroyItself)
        {
            Destroy(gameObject);
        }
    }

    private void ApplyDamageToPlayer(Collision collision)
    {
        // deal damage to the player
        Health playerHealth = collision.collider.GetComponent<Health>();
        playerHealth?.LoseHealth(_damage);
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
