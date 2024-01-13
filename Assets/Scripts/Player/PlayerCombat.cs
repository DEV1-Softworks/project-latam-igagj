using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
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

    private void OnEnable()
    {
        GameInput.Instance.inputActions.Player1.Attack.performed += Attack_performed;   
    }

    private void OnDisable()
    {
        GameInput.Instance.inputActions.Player1.Attack.performed -= Attack_performed;
    }

    private void Attack_performed(InputAction.CallbackContext _)
    {
        if (armed)
        {
            // Spawn a bullet at gunEndPoint, taking player direction into account
            GameObject bullet = Instantiate(pfProjectile, gunEndPoint.position, Quaternion.Euler(0f, 0f, transform.localScale.x == 1f ? 0f : 180f)); // TODO: Readability
            Projectile projectileScript = bullet.GetComponent<Projectile>();
            projectileScript.SetShootForce(shootForceMagnitude);

            smokeParticles.Play();

            ParticleSystem newParticle = Instantiate(sparksParticles, gunEndPoint.position, gunEndPoint.rotation);
            newParticle.Play();
            Destroy(newParticle.gameObject, 1f);
        }
    }
}
