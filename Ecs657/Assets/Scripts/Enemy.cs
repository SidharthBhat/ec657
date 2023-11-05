using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] NavMeshAgent enemy;
    [SerializeField] Transform player;
    [SerializeField] LayerMask groundLayer, playerLayer;
    [SerializeField] Timer timer;
    //_________________________________________________________//
    //Movement variables
    [SerializeField] float waitAtPoint;
    [SerializeField] float walkRange, attackRange, sightRange;
    bool isWalkPointSet = false;
    Vector3 walkPoint;
    //_________________________________________________________//
    //Attack variables
    [SerializeField] GameObject projectile;
    [SerializeField] float projSpeed;
    [SerializeField] float attackTiming;
    [SerializeField] int damage;
    bool hasAttacked = false;
    bool canSeePlayer = false;
    bool canAttackPlayer = false;
    //_________________________________________________________//
    //HP variables
    [SerializeField] HealthBar healthbar;
    [SerializeField] int maxHitPoints;
    int hitPoints;
    [SerializeField] bool isDead;
    //_________________________________________________________//
    //Boss decleration variable
    [SerializeField] bool isBoss;
    //_________________________________________________________//
    //will be used for win screen
    [SerializeField] private Menus menus;
    //_________________________________________________________//
    //xpVariables 
    [SerializeField] private float xpValue;
    [SerializeField] GameObject eXP;
    //_________________________________________________________//



    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        timer = GameObject.Find("Timer").GetComponent<Timer>();
        isDead = false;
        SetStats();
    }

    // Update is called once per frame
    void Update()
    {
        // Checks to see if the object can see or attack the player
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
    //--------------------------------------------------------//
    #region attacking code
    //fire projectile towards player
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

    // Creates a projectile to send towards the player
    private void Attack()
    {
        GameObject currentprojectile = Instantiate(projectile, transform.position + transform.forward, Quaternion.identity);
        currentprojectile.GetComponent<Rigidbody>().AddForce(transform.forward * projSpeed, ForceMode.Impulse);
        currentprojectile.GetComponent<Projectile>().SetDamage(damage);
    }
	#endregion
    //--------------------------------------------------------//
	#region movement code
    //go towards player
	private void ChasePlayer()
    {
        enemy.SetDestination(player.position);
    }

    // Controls enemy movement by setting and guiding them to walk points.
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

    // Sets location to head to
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
    #endregion
    //--------------------------------------------------------//
    #region takeDamage code
    //reduce current hp by x
    public void TakeDamage(int amount)
    {
        healthbar.setHealth(hitPoints);
        hitPoints -= amount;
        if (hitPoints <= 0 && !isDead)
        {
            isDead = true;
            Die();
        }
    }

    //functions needed when dying
    private void Die()
    {
        //cheaks if the enemy is the final boss
        if (isBoss)
        {
            menus.Win();
            Die();
        }
        else 
        {
            TempEnemy();
            DropXP(xpValue);
            Destroy(gameObject);
        }
    }
    //Drops x number of orbs which gives you xpValue worth of xp in total
    //orbs drop randomly when enemy dies within a certain range
    private void DropXP(float value)
	{
        int numberOfXP = Random.Range(1, 5);
        for(int i = 0; i < numberOfXP; i++)
		{
            float randomRangeX = Random.Range(0.5f,-0.5f);
            float randomRangeY = Random.Range(0.5f, -0.5f);
            float randomRangeZ = Random.Range(0.5f, -0.5f);
            Vector3 randomPosition = new Vector3(transform.position.x + randomRangeX, 
                                                 transform.position.y + randomRangeY, 
                                                 transform.position.z + randomRangeZ);

            GameObject XPDrop = Instantiate(eXP, randomPosition, Quaternion.identity);
            XPDrop.GetComponent<ExperienceController>().SetXp(value / numberOfXP);
		}
	}

    #endregion
    //--------------------------------------------------------//
    #region misc code
    //spawn 2 enemies every time an enemy dies
    private void TempEnemy()
    {
        float randomZ = Random.Range(-walkRange, walkRange);
        float randomX = Random.Range(-walkRange, walkRange);

        Vector3 spawnLocation = new Vector3(200f + randomX,
                                            5f,
                                            200f + randomZ);
        Instantiate(gameObject, spawnLocation, Quaternion.identity);
        Instantiate(gameObject, spawnLocation + Vector3.one * 2, Quaternion.identity);
    }

    //For ramping the difficulty of the enemy over time
    private void SetStats()
	{
        float currentTime = timer.timeValue;

        //increase hp every 30 seconds by 5% up to 200% original hp, linear
        int HPIncrementer = Mathf.CeilToInt(currentTime / 30);
        float HPMultiplier = 1 + Mathf.Clamp(HPIncrementer / 20, 0f, 2f);
        maxHitPoints += Mathf.CeilToInt(maxHitPoints * HPMultiplier);
        hitPoints = maxHitPoints;
        healthbar.setMaxHealth(maxHitPoints);

        //increase projectile speed ever 60 seconds by 10% up to 30% increase, linear
        int projectileSpeedIncrementer = Mathf.CeilToInt(currentTime / 60);
        float projectileSpeedMultiplier = 1 + Mathf.Clamp(projectileSpeedIncrementer / 10, 0f, 0.3f);
        projSpeed += Mathf.CeilToInt(projSpeed * projectileSpeedMultiplier);

        //increase damage by 1 every minute up to 5, linear
        int DamageIncrementer = Mathf.CeilToInt(currentTime / 60);
        DamageIncrementer = (int) Mathf.Clamp(DamageIncrementer, 0f, 5f);
        damage += DamageIncrementer;
    }
    //for debugging
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, walkRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

    }

    #endregion
    //--------------------------------------------------------//
}

