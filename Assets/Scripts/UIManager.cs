using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField]
    private GameObject _gameOverPanel;
    [SerializeField]
    private Image _xpSlider;
    [SerializeField]
    private Image _healthSlider;
    [SerializeField]
    private TextMeshProUGUI _score;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        // subscribe to GameManager events
        GameManager.Instance.OnScoreChanged += UpdateScore;
        GameManager.Instance.OnHealthChanged += UpdateHealth;
        GameManager.Instance.OnXPChanged += UpdateXP;
    }

    private void OnDestroy()
    {
        // unsubscribe from GameManager events
        GameManager.Instance.OnScoreChanged -= UpdateScore;
        GameManager.Instance.OnHealthChanged -= UpdateHealth;
        GameManager.Instance.OnXPChanged -= UpdateXP;
    }

    private void UpdateScore(int score)
    {
        _score.text = score + " pts";
    }

    private void UpdateHealth(float health)
    {
        _healthSlider.fillAmount = health / 100f; // fill between 0 & 1
    }

    private void UpdateXP(float xp)
    {
        _xpSlider.fillAmount = xp / 100f;
    }

    public void ShowGameOver()
    {
        _gameOverPanel.SetActive(true);
    }
}
