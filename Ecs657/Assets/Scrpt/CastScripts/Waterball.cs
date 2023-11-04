using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Waterball : GenericProjectile
{
    private bool effectActive = false;
    private float speed;
    public Waterball(int damage, int duration, int interval):base(damage,duration, interval)
    {

    }

    protected override IEnumerator projEffect()
    {
        if(!effectActive)
        {
            enemy.gameObject.GetComponent<NavMeshAgent>().speed/=2;
            effectActive = true;
        }
        else
        {
            if(duration>1)
            {
                enemy.gameObject.GetComponent<NavMeshAgent>().speed *=2;
            }
            duration--;
        }
        yield return new WaitForSeconds(interval);
    }
    
}
