using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Android;

public abstract class GenericProjectile : MonoBehaviour
{
    private int damage;
    protected int duration;
    protected int interval;
    protected Enemy enemy;

    public GenericProjectile(int damage, int duration, int interval)
    {
        this.damage = damage;
        this.duration = duration;
        this.interval = interval;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            enemy = other.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(damage);
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<MeshCollider>().enabled = false;
            StartCoroutine(projEffect());
        }
    }

    protected abstract IEnumerator projEffect();

    // Update is called once per frame
    void Update()
    {
        if (duration <= 0)
        {
            StopCoroutine(projEffect());
            Destroy(gameObject);

        }
    }
}
