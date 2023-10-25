using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SpellStack : MonoBehaviour
{
    [SerializeField] private Spell[] stackSpells;
    [SerializeField] private SlotController[] stackSlots;
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
        checkDuplicates(spell);
        //otherwise, its added to the stack
        stackSpells[stackIndex] = spell;
        stackSlots[stackIndex].SetSpell(spell);
        stackIndex++;
        //checks every spell in spellList for combination match
        foreach (Spell newSpell in spellList)
        {
            //if match found, remove each spell in stack and add the new spell
            if (newSpell.checkCombination(stackSpells))
            {
                foreach(Spell remove in newSpell.combination)
                {
                    removeSpell(remove);
                }
                addSpell(newSpell);
            }
        }
    }

    void checkDuplicates(Spell spell)
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
        }
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
        if (index < stackSpells.Length - 1)
        {
            for(int i = index; i < stackSpells.Length-1; i++)
            {
                stackSpells[i] = stackSpells[i + 1];
                if (stackSpells[i] == null)
                {
                    stackSlots[i].clearSlot();
                }
                else
                {
                    stackSlots[i].SetSpell(stackSpells[i]);
                }
            }
        }
        stackSpells[stackSpells.Length - 1] = null;
        stackSlots[stackSlots.Length - 1].clearSlot();
        stackIndex--;
    }

    void removeSpell(Spell spell)
    {
        for (int i = 0; i < stackSpells.Length; i++)
        {
            if (stackSpells[i].name == spell.name)
            {
                removeSpell(i);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
