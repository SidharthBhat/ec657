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
    }

    public Steam(Sprite i, Spell[] c) : base(i, "Steam", "Shoots ball of steam. Damages and pushes enemy away.", c)
    {
    }

    //spawns steam projectile
    public override void Cast()
    {
        GameObject currentprojectile = Instantiate(steamProj, player.transform.position + player.transform.forward, Quaternion.identity).gameObject;
        currentprojectile.GetComponent<Rigidbody>().AddForce(player.transform.forward * projSpeed, ForceMode.Impulse);
        currentprojectile.GetComponent<Fireball>().setData(damage, duration, interval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
