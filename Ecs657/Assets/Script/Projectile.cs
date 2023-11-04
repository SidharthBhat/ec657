using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float expiration;
    int damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Sets the amount of damage a projectile will do as an int
    public void SetDamage( int amount)
    {
        damage = amount;
    }

    // Update is called once per frame
    void Update()
    {
        Invoke(nameof(DestroyProjectile), expiration);
    }

    // If projectile collides with an enemy call TakeDamage
    void OnCollisionEnter(Collision collide){
        if (collide.gameObject.GetComponent<Enemy>() != null)
        {
            collide.gameObject.GetComponent<Enemy>().TakeDamage(damage);
        }
        if (collide.gameObject.GetComponent<PlayerStats>() != null)
        {
            collide.gameObject.GetComponent<PlayerStats>().TakeDamage(damage);
        }
        
        Destroy(gameObject);
    } 

    // Clears Projectile
    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
