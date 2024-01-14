using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rb;
    private float shootForceMagnitude;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rb.AddForce(transform.position + shootForceMagnitude * transform.localScale.x * transform.right);
        Destroy(gameObject, 1f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out HealthSystem healthSystem))
            healthSystem.TakeDamage();
        Destroy(gameObject);
    }

    public void SetShootForce(float magnitude)
    {
        shootForceMagnitude = magnitude;
    }
}
