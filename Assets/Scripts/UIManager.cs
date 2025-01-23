using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    // static instance
    public static UIManager Instance { get; private set; }

    [SerializeField]
    private GameObject _gameOverPanel;
    [Header("Game UI")]
    [SerializeField]
    private GameObject _xpSlider;
    [SerializeField]
    private GameObject _healthSlider;
    [SerializeField]
    private TextMeshProUGUI _score;

    private void Awake()
    {
        // make sure there is only one instance of UIManager
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // destroy this instance if another one exists
            return;
        }

        Instance = this;

        // make this object persistent between scenes
        DontDestroyOnLoad(gameObject);
    }


    public void ShowGameOver()
    {
        _gameOverPanel.SetActive(true);
    }


}
