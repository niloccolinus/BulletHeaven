using UnityEngine;
using System;

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
}
