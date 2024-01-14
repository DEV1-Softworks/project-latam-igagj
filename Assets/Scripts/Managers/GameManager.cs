using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int lives;

    public Action OnPlayerDied;
    public Action<Player> OnRevivalStarted;
    public Action OnGameOver;

    private void OnEnable()
    {
        OnPlayerDied += GameManager_OnPlayerDied;
    }

    private void OnDisable()
    {
        OnPlayerDied -= GameManager_OnPlayerDied;
    }

    private void Singleton()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    private void Awake()
    {
        Singleton();
    }

    private void GameManager_OnPlayerDied()
    {
        lives--;
        if (lives <= 0)
            OnGameOver?.Invoke();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
