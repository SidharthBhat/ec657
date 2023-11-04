using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
    [SerializeField] private float xpValue;
    [SerializeField] GameObject eXP;
    [SerializeField] private bool debug;
    [SerializeField] HealthBar healthbar;

    void Awake()
    {
        //initialises enemy
        player = GameObject.FindGameObjectWithTag("Player").transform;
        hitPoints = maxHitPoints;
        healthbar.setMaxHealth(maxHitPoints);
    }

    // Update is called once per frame
    void Update()
    {
        canSeePlayer = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        canAttackPlayer = Physics.CheckSphere(transform.position, attackRange, playerLayer);

        //detects player to attack or follow
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

    // attacks player with a delay between attacks
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

    // checks if attacked recently
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

    // Starts heading towards players location
    private void ChasePlayer()
    {
        enemy.SetDestination(player.position);
    }

    // Allows enemy to wander around the map
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

    // Sets a random location to walk to
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

    // Removes an int of damage after colliding with a projectile
    public void TakeDamage(int amount)
    {
        hitPoints -= amount;
        healthbar.setHealth(hitPoints);
        if (hitPoints <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        //add exp drop
        TempEnemy();
        DropXP(xpValue);
        Destroy(gameObject);
    }

    // Drops a random quantity of exp balls
    private void DropXP(float value)
	{
        int numberOfXP = Random.Range(1, 5);
        for(int i = 0; i < numberOfXP; i++)
		{
            float randomRangeX = Random.Range(0.5f,-0.5f);
            float randomRangeY = Random.Range(0.5f, -0.5f);
            float randomRangeZ = Random.Range(0.5f, -0.5f);
            Vector3 randomPosition = new Vector3(transform.position.x + randomRangeX, transform.position.y + randomRangeY, transform.position.z + randomRangeZ);
            GameObject XPDrop = Instantiate(eXP, randomPosition, Quaternion.identity);
            XPDrop.GetComponent<ExperienceController>().SetXp(value / numberOfXP);
		}
	}


    void TempEnemy()
    {
        float randomZ = Random.Range(-walkRange, walkRange);
        float randomX = Random.Range(-walkRange, walkRange);

        Vector3 spawnLocation = new Vector3(200f +randomX,
                                5f,
                                200f +randomZ);

        Instantiate(gameObject, spawnLocation, Quaternion.identity);
        Instantiate(gameObject, spawnLocation + Vector3.one *2, Quaternion.identity);
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

