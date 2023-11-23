using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class Spell : MonoBehaviour
{
    public ScriptableSpell spellType;
    PlayerStats playerStats;
    GameObject player;
    public Sprite icon;
    public ScriptableSpell [] combination;
    public string spellName;

    //interval only functions for repeating effects (e.g. chip damage), so then total duration is duration*interval
    //for one-time effects, (e.g. slowdown), interval is useless so duration is the total time for the effect

    public void InitialiseSpell()
    {
        GameObject camera =GameObject.FindGameObjectWithTag("MainCamera");
        player = GameObject.FindGameObjectWithTag("Player");
        playerStats = player.GetComponent<PlayerStats>();
        combination = spellType.combinations;
        icon = spellType.image;
        spellName = spellType.name;
    }

    //overrides the abstract cast method in spell for this specific spell's behaviour
    public void Cast()
    {
        float dmgMul = playerStats.dmgMul;
        int dmg = Mathf.RoundToInt(spellType.damage*dmgMul);
        
        //for example, here Fire creates a new fireball from the prefab in fireProj, then adds forward force to it, and initialise its stats
        GameObject currentprojectile = Instantiate(spellType.projectile, player.transform.position + player.transform.forward, Quaternion.identity);
        currentprojectile.GetComponent<Rigidbody>().AddForce(player.transform.forward * spellType.projSpeed, ForceMode.Impulse);
        currentprojectile.GetComponent<GenericProjectile>().setData(dmg,spellType.duration,spellType.interval);
    }

    //check combination just gets the spell stack, compares it to it's combination recipe,
    ///and returns the index of the first matching spell for substitution
    public int checkCombination(Spell[] spells)
    {
        bool match = true;
        if(spellType.combinations.Length > 0)
        {
            for (int i = 0; i < (spells.Length - spellType.combinations.Length); i++)
            {
                try
                {
                    //make sure the slot isn't blank && the spell matches the 1st spell in the combination array
                    if (spells[i].spellName != null && spells[i].spellType == spellType.combinations[0])
                    {
                        match = true;
                        //after checking a match for the first spell of the combination,
                        //checks if the following spells match the rest of the combination
                        for (int j = 1; j < spellType.combinations.Length; j++)
                        {
                            if (spellType.combinations[j] != spells[i + j].spellType)
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
                catch (System.Exception)
                {
                    continue;
                    throw;
                }
            }
        }
        return -1;
    }
}
