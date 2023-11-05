using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamethrowerController : MonoBehaviour
{
    public ParticleSystem flameParticles;
    public int damage = 4;
    public float damageInterval = 0.2f; // The time interval for damage in seconds

    private bool canDamage = true;

    void Update()
    {
        if (Input.GetButtonDown("Fire1")) // Change to your desired input method
        {
            StartFlamethrower();
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            StopFlamethrower();
        }
    }

    void StartFlamethrower()
    {
        flameParticles.Play();
    }

    void StopFlamethrower()
    {
        flameParticles.Stop();
    }

    private void OnParticleCollision(GameObject other)
    {
        if (canDamage)
        {
            // Check if the collided object has an "Enemy" component
            Enemy enemy = other.GetComponent<Enemy>();

            if (enemy != null)
            {
                // Apply damage to the enemy
                enemy.TakeDamage(damage);

                // Start the damage cooldown coroutine
                StartCoroutine(DamageCooldown());
            }
        }
    }

    IEnumerator DamageCooldown()
    {
        canDamage = false;
        yield return new WaitForSeconds(damageInterval);
        canDamage = true;
    }
}
