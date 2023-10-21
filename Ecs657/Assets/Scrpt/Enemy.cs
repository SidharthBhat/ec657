using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [SerializeField] NavMeshAgent enemy;
    [SerializeField] Transform player;
    [SerializeField] float waitAtPoint;
    [SerializeField] GameObject projectile;
    [SerializeField] float projSpeed;
    [SerializeField] LayerMask groundLayer, playerLayer;
    [SerializeField] float walkRange, attackRange, sightRange;
    bool isWalkPointSet = false;
    Vector3 walkPoint;
    [SerializeField] float attackTiming;
    bool hasAttacked = false;
    bool canSeePlayer = false;
    bool canAttackPlayer = false;
    [SerializeField] int damage;
    [SerializeField] int maxHitPoints;
    int hitPoints;

    void Awake()
    {
        hitPoints = maxHitPoints;
    }

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
            Invoke(nameof(ResetAttack), attackTiming);
        }
    }

    private void ResetAttack()
    {
        hasAttacked = false;
    }

    private void Attack()
    {
        GameObject currentprojectile = Instantiate(projectile, transform.position + transform.forward, Quaternion.identity);
        currentprojectile.GetComponent<Rigidbody>().AddForce(transform.forward * projSpeed, ForceMode.Impulse);
        currentprojectile.GetComponent<Projectile>().SetDamage(damage);
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

    public void TakeDamage(int amount)
    {
        hitPoints -= amount;
        if (hitPoints < 0)
        {
            Die();
        }
    }

    void Die()
    {
        //add exp drop

        Destroy(gameObject);
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
