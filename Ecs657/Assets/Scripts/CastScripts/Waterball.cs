using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Waterball : GenericProjectile
{
    private bool effectActive = false;
    private float speed;


    protected override IEnumerator projEffect()
    {
        enemyObj.GetComponent<NavMeshAgent>().speed /= 2;
        yield return new WaitForSeconds(duration);
        enemyObj.GetComponent<NavMeshAgent>().speed *= 2;
        duration = 0;
    }
    
}
