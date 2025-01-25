using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private float _maxHealth = 100f;
    [SerializeField]
    private float _currentHealth;


    public bool IsDead = false;

    private void Start()
    {
        _currentHealth = _maxHealth;

        // if game object is the player, synchronize health
        if (gameObject.CompareTag("Player") && GameManager.Instance != null)
        {
            GameManager.Instance.PlayerHealth = _currentHealth;
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
        // if player is dead, show game over
        if (gameObject.CompareTag("Player"))
        {
            UIManager.Instance.ShowGameOver();
            Time.timeScale = 0; // stop game
        }
        else
        {
            IsDead = true;
            StartCoroutine(DeathSequence());
        }
    }

    private IEnumerator DeathSequence()
    {
        // play vfx or animation


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
