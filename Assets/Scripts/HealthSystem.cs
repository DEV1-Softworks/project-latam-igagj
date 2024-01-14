using System;
using System.Collections;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public float revivalTimer;

    [SerializeField] private int health;

    public Action<int> OnHealthChanged;

    public void TakeDamage()
    {
        health--;
        if (health <= 0)
            Die();
        OnHealthChanged?.Invoke(health);
    }

    private void Die()
    {
        if (TryGetComponent(out Player player))
        {
            GameManager.Instance.OnPlayerDied?.Invoke();
            if (GameManager.Instance.lives > 0)
                Revive(player);
        }

        else
            Destroy(gameObject);
    }

    public void Revive(Player player)
    {
        GameManager.Instance.OnRevivalStarted?.Invoke(GetComponent<Player>());
        StartCoroutine(RevivalRoutine(player));
    }

    private IEnumerator RevivalRoutine(Player player)
    {
        player.DestroyComponentsExcept(this);
        while (revivalTimer > 0)
        {
            revivalTimer -= Time.deltaTime;
            yield return null;

        }
        SpawnManager.Instance.SpawnPlayer(player);
    }
}
