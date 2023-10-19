using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] NavMeshAgent enemy;
    [SerializeField] Transform player;
    [SerializeField] float waitAtPoint;
    [SerializeField] LayerMask groundLayer, playerLayer;
    [SerializeField] float walkRange, attackRange, sightRange;
    bool isWalkPointSet = false;
    Vector3 walkPoint;
    [SerializeField] float attackTimeing;
    bool hasAttacked = false;
    bool canSeePlayer = false;
    bool canAttackPlayer = false;



    // Update is called once per frame
    void Update()
    {
        canSeePlayer = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        canAttackPlayer = Physics.CheckSphere(transform.position, attackRange, playerLayer);

        if (canSeePlayer)
        {
            if(canAttackPlayer)
            {
                AttackPlayer();
            }
            else
            {
                ChasePlayer();
            }
        }
        else
        {
            Wonder();
        }
    }

    private void AttackPlayer()
    {
        enemy.SetDestination(transform.position);
        transform.LookAt(player.position);

        if (!hasAttacked)
        {
            Attack();

            hasAttacked = true;
            Invoke(nameof(ResetAttack), attackTimeing);
        }
    }

    private void ResetAttack()
    {
        hasAttacked = false;
    }

    private void Attack()
    {
        Debug.Log("Wack");
    }

    private void ChasePlayer()
    {
        enemy.SetDestination(player.position);
    }

    private void Wonder()
    {
        if(isWalkPointSet)
        {
           enemy.SetDestination(walkPoint);
           Vector3 distanceToWalkPoint = transform.position - walkPoint; 

           if (distanceToWalkPoint.magnitude < 1f)
           {
            isWalkPointSet = false;
           }
        }
        else
        {
            Invoke(nameof(SetWalkingPoint), Random.Range(0f, waitAtPoint));
        }
    }

    private void SetWalkingPoint()
    {
        float randomZ = Random.Range(-walkRange, walkRange);
        float randomX = Random.Range(-walkRange, walkRange);

        walkPoint = new Vector3(transform.position.x + randomX,
                                transform.position.y,
                                transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, groundLayer))
        {
            isWalkPointSet = true;
        }
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, walkRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

    }


}
