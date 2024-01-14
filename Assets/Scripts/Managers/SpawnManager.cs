using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;

    [Header("Prefabs")]
    [SerializeField] private GameObject pfEnemy;
    [SerializeField] private GameObject pfPlayer1;
    [SerializeField] private GameObject pfPlayer2;
    [Space]
    [Header("Settings")]
    [SerializeField] private Transform[] enemySpawnPositions;
    [SerializeField] private Transform playerSpawnPos;
    [SerializeField] private float spawnRate;

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

    private void Start()
    {
        StartCoroutine(SpawnEnemiesRecursive());
    }

    private IEnumerator SpawnEnemiesRecursive()
    {
        while (true)
        {
            Instantiate(pfEnemy, enemySpawnPositions[Random.Range(0, enemySpawnPositions.Length)].position, Quaternion.identity);
            yield return new WaitForSeconds(1f/spawnRate);
        }
    }

    public void SpawnPlayer(Player player)
    {
        switch (player.number)
        {
            case Player.Number.One:
                Instantiate(pfPlayer1, playerSpawnPos.position, playerSpawnPos.rotation);
                break;
            case Player.Number.Two:
                Instantiate(pfPlayer2, playerSpawnPos.position, playerSpawnPos.rotation);
                break;
        }
        Destroy(player.gameObject);
    }
}
