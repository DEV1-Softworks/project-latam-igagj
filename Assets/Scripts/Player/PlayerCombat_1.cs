using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat_1 : MonoBehaviour
{
    [Header("Weapon")]
    [SerializeField] private Transform gunEndPoint;
    [SerializeField] private bool armed = true;
    [SerializeField] private float shootForceMagnitude;
    [SerializeField] private GameObject pfProjectile;
    [Space]
    [Header("VFX")]
    [SerializeField] private ParticleSystem smokeParticles;
    [SerializeField] private ParticleSystem sparksParticles;
    [Space]
    [Header("Settings")]
    [SerializeField] private LayerMask playerLayer;

    private void OnEnable()
    {
        GameInput.Instance.inputActions.Player1.Attack.performed += Attack_performed;   
    }

    private void OnDisable()
    {
        GameInput.Instance.inputActions.Player1.Attack.performed -= Attack_performed;
    }

    // Spawn a projectile that ignores players, at gunEndPoint, taking player direction into account
    private void Attack_performed(InputAction.CallbackContext _)
    {
        if (armed)
        {
            float gunAngle = transform.localScale.x == 1f ? 0f : 180f;
            Quaternion gunRotation = Quaternion.Euler(0f, 0f, gunAngle);

            GameObject projectile = Instantiate(pfProjectile, gunEndPoint.position, gunRotation);
            
            // Modify projectile
            Projectile projectileScript = projectile.GetComponent<Projectile>();
            projectileScript.SetShootForce(shootForceMagnitude);
            projectileScript.GetComponent<Rigidbody2D>().excludeLayers = playerLayer;

            // Play particle systems
            smokeParticles.Play();
            ParticleSystem newParticle = Instantiate(sparksParticles, gunEndPoint.position, gunEndPoint.rotation);
            newParticle.Play();
            Destroy(newParticle.gameObject, 1f);
        }
    }
}
