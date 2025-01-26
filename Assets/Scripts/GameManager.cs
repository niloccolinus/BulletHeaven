using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField]
    private int _totalScore;
    [SerializeField]
    private float _playerHealth;
    [SerializeField]
    private float _playerXP;

    public event Action<int> OnScoreChanged;
    public event Action<float> OnHealthChanged;
    public event Action<float> OnXPChanged;

    private void Awake()
    {
        // make sure there is only one instance of GameManager
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // destroy this instance if another one exists
            return;
        }

        Instance = this;
        // make this object persistent between scenes
        DontDestroyOnLoad(gameObject);
    }

    // properties to handle global data with events
    public int TotalScore
    {
        get => _totalScore;
        set
        {
            _totalScore = value;
            OnScoreChanged?.Invoke(_totalScore);
        }
    }

    public float PlayerHealth
    {
        get => _playerHealth;
        set
        {
            _playerHealth = Mathf.Clamp(value, 0, 100);
            OnHealthChanged?.Invoke(_playerHealth);
        }
    }

    public float PlayerXP
    {
        get => _playerXP;
        set
        {
            _playerXP = value;
            OnXPChanged?.Invoke(_playerXP); 
        }
    }

    public void TriggerGameOver()
    {
        Time.timeScale = 0; // stop game time
        UIManager.Instance.ShowGameOver();
    }

    public void ResetGame()
    {
        // reset game data
        TotalScore = 0;
        PlayerHealth = 100f;
        PlayerXP = 0f;

        // update the UI
        UIManager.Instance.ResetUI();
    }

    public void ConsumeXP(float xpToConsume)
    {
        // reduce XP based on the consumption rate & ensure XP does not drop below zero
        GameManager.Instance.PlayerXP = Mathf.Max(0, GameManager.Instance.PlayerXP - xpToConsume * Time.deltaTime);
    }
}
