using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class Spell : MonoBehaviour
{
    public Sprite icon;
    public string spellName;
    public string description;
    public Spell[] combination;
    protected GameObject player;

    public Spell(Sprite i, string sn, string d, Spell[] c, GameObject p)
    {
        icon = i;
        spellName = sn;
        description = d;
        combination = c;
        player = p;
    }

    public abstract void Cast();


    public bool checkCombination(Spell[] spells)
    {
        foreach (Spell spell in spells)
        {
            if (!combination.Contains(spell))
            {
                return false;
            }
        }
        return true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
