using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    private HealthSystem healthSystem;
    private Canvas worldCanvas;
    private Image healthImage;

    private void OnEnable()
    {
        healthSystem.OnHealthChanged += UpdateUI;
    }

    private void OnDisable()
    {
        healthSystem.OnHealthChanged -= UpdateUI;
    }

    private void Awake()
    {
        healthSystem = GetComponentInParent<HealthSystem>();
        worldCanvas = GetComponentInChildren<Canvas>();
        healthImage = GetComponentInChildren<Image>();
    }

    private void Start()
    {
        UpdateUI();
    }

    private void LateUpdate()
    {
        worldCanvas.transform.localScale = healthSystem.transform.localScale;
    }

    private void UpdateUI(int newHealth = 5)
    {
        if (newHealth <= 0)
            worldCanvas.gameObject.SetActive(false);

        healthImage.fillAmount = newHealth / 5f;
    }
}
