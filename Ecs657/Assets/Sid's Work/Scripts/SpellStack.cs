using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SpellStack : MonoBehaviour
{
    [SerializeField] private Spell[] stackSpells;
    [SerializeField] private GameObject[] stackSlots = new GameObject[5];
    [SerializeField] private Transform slotFab;
    [SerializeField] private GameObject grid;
    private int stackIndex = 0;
    [SerializeField] private Spell[] spellList;
    [SerializeField] private GameObject spellListObj;

    public Stack<Spell> XspellStack;

    // Start is called before the first frame update
    void Start()
    {
        spellList = spellListObj.GetComponent<SpellList>().spellList;
    }

    public void addSpell(Spell spell)
    {
        //if spell is added twice, it'll cancel out
        if (!checkDuplicates(spell))
        {
            //otherwise, its added to the stack
            stackSpells[stackIndex] = spell;
            stackSlots[stackIndex] = Instantiate(slotFab, grid.transform).gameObject;
            stackSpells[stackIndex].GetComponent<SlotController>().SetSpellInit(spell);
            stackIndex++;
        }
        //check if any new combos are possible
        checkCombos();
    }

    public void checkCombos()
    {
        //checks every spell in spellList for combination match
        foreach (Spell newSpell in spellList)
        {
            //if match found, remove each spell in stack and add the new spell
            int index = newSpell.checkCombination(stackSpells);
            if (index >= 0)
            {
                for (int i = 0; i < newSpell.combination.Length; i++)
                {
                    removeSpell(index);
                }
                addSpell(newSpell);
            }
        }
        //this should mean that whenever a new spell is added, or a duplicate is removed, it'll check for combos
        //this can lead to compound spells through stacking combination
        //e.g. add fire, earth, water, remove earth, fire and water combine to make steam
        //and when steam is added, it can possible combine with other already present elements
        //e.g. fire,fire,earth,water, remove earth for fire,fire,water, fire and water make steam, fire and steam make Flame Spray
    }

    bool checkDuplicates(Spell spell)
    {
        int index = -1;

        for (int i = 0; i < stackSlots.Length; i++)
        {
            if (stackSlots[i].name == spell.name)
            {
                index = i;
                break;
            }
        }
        
        if(index > 0)
        {
            removeSpell(index);
            return true;
        }

        return false;
    }

    int checkIndex(Spell spell)
    {
        for(int i = 0; i < stackSpells.Length; i++)
        {
            if (stackSpells[i].name == spell.name)
            {
                return i;
            }
        }
        return -1;
    }

    void removeSpell(int index)
    {
        if (index < stackSpells.Length - 1 && index>=0)
        {
            Destroy(stackSlots[index]);
            stackSlots[index] = null;
            stackSpells[index] = null;
            for (int i = index; i < stackSpells.Length - 1; i++)
            {
                stackSlots[i] = stackSlots[i + 1];
                stackSpells[i] = stackSpells[i + 1];
                if (stackSpells[i] == null)
                {
                    break;
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
