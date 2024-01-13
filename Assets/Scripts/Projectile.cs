using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rb;
    private float shootForceMagnitude;
    [SerializeField] private int bulletDamage;

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
            healthSystem.TakeDamage(bulletDamage);
        Destroy(gameObject);
    }

    public void SetShootForce(float magnitude)
    {
        shootForceMagnitude = magnitude;
    }
}
