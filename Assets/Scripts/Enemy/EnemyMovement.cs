using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;

    private void Update()
    {
        // TODO: Readability
        transform.Translate(movementSpeed * Time.deltaTime * (new Vector3(GetAnyPlayerXPos(), transform.position.y) - transform.position).normalized);
    }

    // TODO: Get closest player!
    private float GetAnyPlayerXPos()
    {
        // Inefficient!
        if (FindAnyObjectByType<Player>() != null)
            return FindAnyObjectByType<Player>().transform.position.x;
        return transform.position.x;
    }
}
