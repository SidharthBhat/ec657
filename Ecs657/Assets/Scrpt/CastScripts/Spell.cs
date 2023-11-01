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

    public Spell(Sprite i, string sn, string d, Spell[] c)
    {
        icon = i;
        spellName = sn;
        description = d;
        combination = c;
    }

    protected void SetPlayer(GameObject player)
    {
        this.player = player;
    }

    public abstract void Cast();


    public int checkCombination(Spell[] spells)
    {
        bool match = true;
        if(combination.Length > 0)
        {
            for (int i = 0; i < (spells.Length - combination.Length); i++)
            {
                if (spells[i]!=null && spells[i].Equals(combination[0]))
                {
                    match = true;
                    for (int j = 1; j < combination.Length; j++)
                    {
                        if (combination[j] != spells[i + j])
                        {
                            match = false;
                            break;
                        }
                    }
                    if (match)
                    {
                        return i;
                    }
                }
            }
        }
        return -1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
