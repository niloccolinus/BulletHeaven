using UnityEngine;

public class GameManager : MonoBehaviour
{
    // static instance
    public static GameManager Instance { get; private set; }


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

}
