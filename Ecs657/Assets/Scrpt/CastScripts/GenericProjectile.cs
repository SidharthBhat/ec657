using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Android;

public abstract class GenericProjectile : MonoBehaviour
{
    private int damage;
    protected int duration;
    protected int interval;
    protected Enemy enemy;
    protected GameObject enemyObj;
    protected bool hit=false;

    public void setData(int damage, int duration, int interval)
    {
        this.damage = damage;
        this.duration = duration;
        this.interval = interval;
        Invoke("cleanup", 5);
    }

    private void cleanup()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        enemyObj = other.gameObject;
        enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            CancelInvoke("cleanup");
            Debug.Log("Collided");
            enemy = other.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(damage);
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<SphereCollider>().enabled = false;
            StartCoroutine(projEffect());
            hit = true;
        }
    }

    protected abstract IEnumerator projEffect();

    // Update is called once per frame
    void Update()
    {
        if (duration <= 0 && hit)
        {
            StopCoroutine(projEffect());
            Destroy(gameObject);

        }
    }
}
