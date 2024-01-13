using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject pfEnemy;
    [SerializeField] private Transform[] spawnPositions;
    [SerializeField] private float spawnRate;

    private void Start()
    {
        StartCoroutine(SpawnEnemiesRecursive());
    }

    private IEnumerator SpawnEnemiesRecursive()
    {
        while (true)
        {
            Instantiate(pfEnemy, spawnPositions[Random.Range(0, spawnPositions.Length)].position, Quaternion.identity);
            yield return new WaitForSeconds(1f/spawnRate);
        }
    }
}
