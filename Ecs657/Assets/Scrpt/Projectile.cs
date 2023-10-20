using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float expiration;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Invoke(nameof(DestroyProjectile), expiration);
    }

    void OnCollisionEnter(Collision collide){
        Destroy(gameObject);
    } 

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
