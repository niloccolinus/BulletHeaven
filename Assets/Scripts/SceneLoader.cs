using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    private string _gameSceneName;
    [SerializeField]
    private GameObject _mainMenuPanel;

    public void StartGame()
    {
        // load scene asynchronously 
        StartCoroutine(LoadGameScene());
    }

    private IEnumerator LoadGameScene()
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(_gameSceneName, LoadSceneMode.Additive);

        while (!loadOperation.isDone)
        {
            yield return null;
        }

        // disable ui
        if (_mainMenuPanel != null) _mainMenuPanel.SetActive(false);

        // set new scene as active once loaded
        Scene newScene = SceneManager.GetSceneByName(_gameSceneName);
        SceneManager.SetActiveScene(newScene);
    }
}
