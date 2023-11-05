using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

//when creating new spells, you need it to use Spell as a namespace, not MonoBehaviour. Spell already has that.
public class Fire : Spell
{
    [SerializeField] Transform fireProj;
    [SerializeField] private float projSpeed;
    [SerializeField] private float cooldown;

    //values for the projectile
    [SerializeField] private int damage; //damage on contact
    [SerializeField] private int duration;
    [SerializeField] private int interval;
    //interval only functions for repeating effects (e.g. chip damage), so then total duration is duration*interval
    //for one-time effects, (e.g. slowdown), interval is useless so duration is the total time for the effect
    
    void Start()
    {
        base.SetPlayer(GameObject.FindGameObjectWithTag("MainCamera"));
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        base.setStats(player.GetComponent<PlayerStats>());
    }

    //constructor for the spell. passes its attributes are arguments to the base Spell class
    //this means you can copy-paste this, and sub in the new spell's classname and its attributes
    //i is the icon, c is the combination recipe, these are both set in the inspector
    public Fire(Sprite i, Spell[] c) : base(i,"Fire","Shoots fireball. Damages and burns enemy.", c)
    {
    }

    //overrides the abstract cast method in spell for this specific spell's behaviour
    public override void Cast()
    {
        float dmgMul = playerStats.dmgMul;
        int dmg = Mathf.RoundToInt(damage*dmgMul);

        //for example, here Fire creates a new fireball from the prefab in fireProj, then adds forward force to it, and initialise its stats
        GameObject currentprojectile = Instantiate(fireProj, player.transform.position + player.transform.forward, Quaternion.identity).gameObject;
        currentprojectile.GetComponent<Rigidbody>().AddForce(player.transform.forward * projSpeed, ForceMode.Impulse);
        currentprojectile.GetComponent<Fireball>().setData(dmg,duration,interval);
    }

}
