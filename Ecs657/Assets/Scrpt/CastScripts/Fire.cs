using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

//when creating new spells, you need it to use Spell as a namespace, not MonoBehaviour. Spell already has that.
public class Fire : Spell
{
    [SerializeField] Transform fireProj;
    [SerializeField] float projSpeed;
    [SerializeField] float cooldown;

    [SerializeField] int damage;
    [SerializeField] int duration;
    [SerializeField] int interval;
    
    void Start()
    {
        base.SetPlayer(GameObject.FindGameObjectWithTag("MainCamera"));
    }

    //constructor for the spell. passes its attributes are arguments to the base Spell class
    //this means you can copy-paste this, and sub in the new spell's classname and its attributes
    //i is the icon, c is the combination recipe
    public Fire(Sprite i, Spell[] c) : base(i,"Fire","Shoots fireball. Damages and burns enemy.", c)
    {
    }

    //overrides the abstract cast method in spell for this specific spell's behaviour
    public override void Cast()
    {
        //for example, here Fire creates a new fireball from the prefab in fireProj, then adds forward force to it
        GameObject currentprojectile = Instantiate(fireProj, player.transform.position + player.transform.forward, Quaternion.identity).gameObject;
        currentprojectile.GetComponent<Rigidbody>().AddForce(player.transform.forward * projSpeed, ForceMode.Impulse);
        currentprojectile.GetComponent<Fireball>().setData(damage,duration,interval);
    }

}
