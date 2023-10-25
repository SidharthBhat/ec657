using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : Spell
{
    [SerializeField] Transform waterProj;
    [SerializeField] float projSpeed;
    [SerializeField] float cooldown;

    // Start is called before the first frame update
    void Start()
    {
        base.SetPlayer(GameObject.FindGameObjectWithTag("Player"));
    }

    public Water(Sprite i, Spell[] c) : base(i, "Water", "Shoots ball of water. Damages and slows enemy.", c) 
    {
    }

    public override void Cast()
    {
        GameObject currentprojectile = Instantiate(waterProj, player.transform.position + player.transform.forward, Quaternion.identity).gameObject;
        currentprojectile.GetComponent<Rigidbody>().AddForce(player.transform.forward * projSpeed, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
