using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Fire : Spell
{
    [SerializeField] Transform fireProj;
    [SerializeField] float projSpeed;
    [SerializeField] float cooldown;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public Fire(Sprite i, Spell[] c) : base(i,"Fire","Shoots fireball. Damages and burns enemy.", c,GameObject.FindGameObjectWithTag("Player"))
    {
    }

    public override void Cast()
    {
        GameObject currentprojectile = Instantiate(fireProj, player.transform.position + player.transform.forward, Quaternion.identity).gameObject;
        currentprojectile.GetComponent<Rigidbody>().AddForce(player.transform.forward * projSpeed, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
