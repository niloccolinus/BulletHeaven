using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private float _maxHealth = 100f;
    [SerializeField]
    private float _currentHealth;
    [SerializeField]
    private int _level;

    private Animator _animator;
    private SatelliteManager _satelliteManager;

    public bool IsDead = false;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _satelliteManager = GetComponent<SatelliteManager>();
        _currentHealth = _maxHealth;

        // if game object is the player, synchronize health
        if (gameObject.CompareTag("Player") && GameManager.Instance != null)
        {
            GameManager.Instance.PlayerHealth = _currentHealth;
        }
        else if (gameObject.CompareTag("Enemy") && GameManager.Instance != null)
        {
            GameManager.Instance.OnEnemiesLevelUp += ScaleHealth;
        }
    }

    private void ScaleHealth(float healthMultiplier, float damageMultiplier)
    {
        if (gameObject.CompareTag("Enemy"))
        {
            _maxHealth *= healthMultiplier;
            _currentHealth = _maxHealth; // reset life to max
        }
    }

    public void LoseHealth(float amount)
    {
        _currentHealth -= amount;
        if (_currentHealth < 0)
        {
            _currentHealth = 0;
        }

        if (gameObject.CompareTag("Player") && GameManager.Instance != null)
        {
            GameManager.Instance.PlayerHealth = _currentHealth;
        }

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    public void GainHealth(float amount)
    {
        _currentHealth = Mathf.Clamp(_currentHealth + amount, 0, _maxHealth); // clamp to max health
        if (_currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth;
        }

        if (gameObject.CompareTag("Player") && GameManager.Instance != null)
        {
            GameManager.Instance.PlayerHealth = _currentHealth;
        }
    }

    private void Die()
    {
        if (gameObject.CompareTag("Enemy") && GameManager.Instance != null)
        {
            GameManager.Instance.OnEnemiesLevelUp -= ScaleHealth;
        }

        if (gameObject.CompareTag("Player"))
        {
            // if player is dead
            StartCoroutine(PlayerDeathSequence());
        }
        else
        {
            // if ememy is dead,
            StartCoroutine(EnemyDeathSequence());
        }
    }

    private IEnumerator PlayerDeathSequence()
    {
        // make robot parts fall (bad performances / requires mesh collider on every part)
        // Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();
        // foreach (var rigidbody in rigidbodies)
        // {
        //    rigidbody.isKinematic = false;
        // }

        IsDead = true;
        _animator.SetBool("IsDying", IsDead);
        _satelliteManager.StopSatellites(); // make satellite fall using gravity

        yield return new WaitForSeconds(1.5f);

        // show game over
        UIManager.Instance.ShowGameOver();
        GameManager.Instance.TriggerGameOver();
    }

    private IEnumerator EnemyDeathSequence()
    {
        IsDead = true;

        // disable meshes & collider
        var collider = GetComponent<Collider>();
        var meshes = GetComponentsInChildren<MeshRenderer>();

        collider.enabled = false;
        foreach (var mesh in meshes)
        {
            mesh.enabled = false;
        }

        yield return new WaitForSeconds(1f);
        Destroy(gameObject); // destroy enemy
    }
}
