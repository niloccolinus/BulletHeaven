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
    [SerializeField]
    private int _level = 1;
    [SerializeField]
    private float _xpThreshold = 100f; // xp needed to level up
    [SerializeField] 
    private bool _boostUnlocked = false;
    [SerializeField]
    private bool _laserUnlocked = false;
    [SerializeField]
    private int _maxEnemies;
    public int MaxEnemies { get => _maxEnemies; }


    public event Action<int> OnScoreChanged;
    public event Action<float> OnHealthChanged;
    public event Action<float> OnXPChanged;
    public event Action<int> OnLevelUp;
    public event Action<int> OnSatelliteGained;
    public event Action<bool> OnBoostUnlocked;
    public event Action<bool> OnLaserUnlocked;


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
            _playerXP = Mathf.Clamp(value, 0, _xpThreshold);

            if (_playerXP >= _xpThreshold)
            {
                _playerXP = 0; // reset xp
                LevelUp();
            }

            OnXPChanged?.Invoke(_playerXP); 
        }
    }

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
        _level = 1;
        _boostUnlocked = false;

        // update the UI
        UIManager.Instance.ResetUI();
    }

    private void LevelUp()
    {
        _level++;
        OnLevelUp?.Invoke(_level);

        // Play level-up sound
        SoundManager.PlaySound(SoundType.LEVELUP);

        // TODO : notify power ups with ui
        if (_level == 2)
        {
            Debug.Log("You unlocked the boost! Press MAJ while moving");

            _boostUnlocked = true;
            OnBoostUnlocked?.Invoke(true); 
        }
        else if (_level > 2 && _level < 11)
        {
            Debug.Log("You earned 1 satellite!");

            OnSatelliteGained?.Invoke(1);
        }
        else if (_level >= 11)
        {
            Debug.Log("You unlocked the laser! Press Spacebar");

            _laserUnlocked = true;
            OnLaserUnlocked?.Invoke(true); 
        }
    }

}
