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
        base.SetPlayer(GameObject.FindGameObjectWithTag("Player"));
    }

    public Fire(Sprite i, Spell[] c) : base(i,"Fire","Shoots fireball. Damages and burns enemy.", c)
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
