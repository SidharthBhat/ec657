using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamethrowerController : MonoBehaviour
{
    public ParticleSystem flameParticles;
    public int damage = 10;

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
    // Detect collisions with the Particle System
    void OnParticleCollision(GameObject other)
    {
        Debug.Log("Particle collided with: " + other.name);
        if (other.gameObject.GetComponent<Enemy>() != null)
        {
            other.gameObject.GetComponent<Enemy>().TakeDamage(damage);
        }

    }
}