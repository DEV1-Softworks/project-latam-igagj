using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [SerializeField] private float knockbackMagnitude;

    // TO-DO: Readability
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Player player))
        {
            HealthSystem playerHealthSystem = player.GetComponent<HealthSystem>();
            playerHealthSystem.TakeDamage();

            Vector2 playerDirection = (player.transform.position - transform.position);

            Rigidbody2D playerRB = player.GetComponent<Rigidbody2D>();
            playerRB.AddForce(playerDirection * knockbackMagnitude, ForceMode2D.Impulse);
        }
    }
}
