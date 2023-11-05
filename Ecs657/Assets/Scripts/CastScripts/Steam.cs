using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steam : Spell {

    [SerializeField] Transform steamProj;
    [SerializeField] float projSpeed;
    [SerializeField] float cooldown;

    [SerializeField] int damage;
    [SerializeField] int duration;
    [SerializeField] int interval;

    // Start is called before the first frame update
    void Start()
    {
        base.SetPlayer(GameObject.FindGameObjectWithTag("MainCamera"));
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        base.setStats(player.GetComponent<PlayerStats>());
    }

    public Steam(Sprite i, Spell[] c) : base(i, "Steam", "Shoots ball of steam. Damages and pushes enemy away.", c)
    {
    }

    //spawns steam projectile
    public override void Cast()
    {
        float dmgMul = playerStats.dmgMul;
        int dmg = Mathf.RoundToInt(damage * dmgMul);

        GameObject currentprojectile = Instantiate(steamProj, player.transform.position + player.transform.forward, Quaternion.identity).gameObject;
        currentprojectile.GetComponent<Rigidbody>().AddForce(player.transform.forward * projSpeed, ForceMode.Impulse);
        currentprojectile.GetComponent<Fireball>().setData(dmg, duration, interval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
