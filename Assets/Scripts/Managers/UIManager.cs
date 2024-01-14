using System.Collections;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Canvas mainCanvas;
    [SerializeField] private Canvas gameOverCanvas;
    [SerializeField] private TMP_Text livesText;
    [SerializeField] private TMP_Text playerOneTimerText;
    [SerializeField] private TMP_Text playerTwoTimerText;

    private void OnEnable()
    {
        GameManager.Instance.OnPlayerDied += UpdateLives;
        GameManager.Instance.OnRevivalStarted += OnRevivalStarted;
        GameManager.Instance.OnGameOver += OnGameOver;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnPlayerDied -= UpdateLives;
        GameManager.Instance.OnRevivalStarted -= OnRevivalStarted;
        GameManager.Instance.OnGameOver -= OnGameOver;
    }

    private void Start()
    {
        UpdateLives();
    }

    private void UpdateLives()
    {
        livesText.text = $"x{GameManager.Instance.lives}";
    }

    private void OnRevivalStarted(Player player)
    {
        StartCoroutine(RevivalTimerRoutine(player));
    }

    private void OnGameOver()
    {
        gameOverCanvas.gameObject.SetActive(true);
        mainCanvas.gameObject.SetActive(false);
    }

    private IEnumerator RevivalTimerRoutine(Player player)
    {
        HealthSystem playerHealthSystem = player.GetComponent<HealthSystem>();

        switch (player.number)
        {
            case Player.Number.One:

                playerOneTimerText.gameObject.SetActive(true);
                while (playerHealthSystem.revivalTimer > 0)
                {
                    playerOneTimerText.text = $"{playerHealthSystem.revivalTimer:N1}";
                    yield return null;
                }
                playerOneTimerText.gameObject.SetActive(false);

                break;
            case Player.Number.Two:

                playerTwoTimerText.gameObject.SetActive(true);
                while (playerHealthSystem.revivalTimer > 0)
                {
                    playerTwoTimerText.text = $"{playerHealthSystem.revivalTimer:N1}";
                    yield return null;
                }
                playerTwoTimerText.gameObject.SetActive(false);

                break;
        }
    }
}