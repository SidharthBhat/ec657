using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewFlameThrower : Spell
{

    public ParticleSystem flameParticles;
    public int damage = 10;
    public float damageInterval = 0.02f; // The time interval for damage in seconds
    private float dmgMul;

    private bool canDamage = true;

    public NewFlameThrower(Sprite i, Spell[] c) : base(i, "Flamethrower", "Shoots jet of flame. Continuously burns the enemy.",c)
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        base.SetPlayer(GameObject.FindGameObjectWithTag("MainCamera"));
        flameParticles.Stop();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        base.setStats(player.GetComponent<PlayerStats>());
    }

    public override void Cast()
    {
        dmgMul = playerStats.dmgMul;
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
            int dmg = Mathf.RoundToInt(damage * dmgMul);

            // Check if the collided object has an "Enemy" component
            Enemy enemy = other.GetComponent<Enemy>();

            if (enemy != null)
            {
                // Apply damage to the enemy
                enemy.TakeDamage(dmg);

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
