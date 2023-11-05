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
    public Spell[] combination; //list of spells and their order needed to create this spell
    protected GameObject player;
    protected PlayerStats playerStats;

    //Spell is abstract, it's not meant to be initialised
    //Instead, actual spells extend it, using the constructor for their attributes
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

    protected void setStats(PlayerStats playerStats)
    {
        this.playerStats = playerStats;
    }

    //cast is implemented in other spells with their own behaviours
    public abstract void Cast();

    //check combination just gets the spell stack, compares it to it's combination recipe,
    ///and returns the index of the first matching spell for substitution
    public int checkCombination(Spell[] spells)
    {
        bool match = true;
        if(combination.Length > 0)
        {
            for (int i = 0; i < (spells.Length - combination.Length); i++)
            {
                //make sure the slot isn't blank && the spell matches the 1st spell in the combination array
                if (spells[i]!=null && spells[i].Equals(combination[0]))
                {
                    match = true;
                    //after checking a match for the first spell of the combination,
                    //checks if the following spells match the rest of the combination
                    for (int j = 1; j < combination.Length; j++)
                    {
                        if (combination[j] != spells[i + j])
                        {
                            match = false;
                            break;
                        }
                    }
                    //if everything matches, and not just the first spell
                    if (match)
                    {
                        return i;
                    }
                }
            }
        }
        return -1;
    }
}
