using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steam : Spell {

    [SerializeField] Transform steamProj;
    [SerializeField] float projSpeed;
    [SerializeField] float cooldown;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public Steam(Sprite i, Spell[] c) : base(i, "Steam", "Shoots ball of steam. Damages and pushes enemy away.", c, GameObject.FindGameObjectWithTag("Player"))
    {
    }

    public override void Cast()
    {
        GameObject currentprojectile = Instantiate(steamProj, player.transform.position + player.transform.forward, Quaternion.identity).gameObject;
        currentprojectile.GetComponent<Rigidbody>().AddForce(player.transform.forward * projSpeed, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
