using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class HotBarController : MonoBehaviour
{
    //here the inputtable spells are stored, and they're added to SpellStack on input
    [SerializeField] private Transform slotFab;
    private SlotController[] slots = new SlotController[10];
    private Spell[] spells = new Spell[10];
    [SerializeField] GameObject spellList;
    [SerializeField] private SpellStack spellStack;
    [SerializeField] private GameObject grid;

    void Start()
    {
        spells=spellList.GetComponent<SpellList>().hotbarList; //retrieves list of spells for the hotbar from spellList
        //sets all slot icons to spells stored on launch if not blank
        for (int i = 0; i < spells.Length; i++)
        {
            if (spells[i] != null)
            {
                slots[i] = Instantiate(slotFab, grid.transform).GetComponent<SlotController>(); //if the slot has a spell, create a SpellSlot
                slots[i].SetSpellInit(spells[i]); //assign the new slot its corresponding spell
            }
        }
    }

    public void AddSpell(int slot)
    {
        //add spell to spellstack
        spellStack.addSpell(spells[slot]);
    }

    void Update()
    {
        //TEMPORARY just gets raw key input for spellcasting
        //Only goes up to three slots, remove later for PlayInput integration
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            AddSpell(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            AddSpell(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            AddSpell(2);
        }
    }
}
