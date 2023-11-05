using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewFlameThrower : Spell
{

    public ParticleSystem flameParticles;
    public int damage = 10;
    public float damageInterval = 0.02f; // The time interval for damage in seconds

    private bool canDamage = true;

    public NewFlameThrower(Sprite i, Spell[] c) : base(i, "Flamethrower", "Shoots jet of flame. Continuously burns the enemy.",c)
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        base.SetPlayer(GameObject.FindGameObjectWithTag("MainCamera"));
        flameParticles.Stop();
    }

    public override void Cast()
    {
        StartCoroutine(StartFlamethrower(4));
    }

    IEnumerator StartFlamethrower(int time)
    {
        //note: currently set to turn off automatically, will later add a check to turn off when player lets go
        flameParticles.Play();
        yield return new WaitForSeconds(time);
        flameParticles.Stop();
    }

    // Detect collisions with the Particle System
    void OnParticleCollision(GameObject other)
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
