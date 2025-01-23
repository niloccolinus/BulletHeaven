using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private float _health;
    [SerializeField]
    private GameObject _gameOverPanel;

    private void Die()
    {
        if (gameObject.CompareTag("Player"))
        {
            UIManager.Instance.ShowGameOver(); // show game over
            Time.timeScale = 0; // stop game
        }
        else Destroy(gameObject);
    }

    public void LoseHealth(float amount)
    {
        _health -= amount;

        if (_health <= 0)
        {
            Die();
        }
    }

    public void GainHealth(int amount)
    {
        _health += amount;
    }
}
