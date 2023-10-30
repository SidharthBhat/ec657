using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class HotBarController : MonoBehaviour
{
    [SerializeField] private Transform slotFab;
    //hotbar slots
    private SlotController[] slots = new SlotController[10];
    //spells in hotbar
    private Spell[] spells = new Spell[10];
    [SerializeField] GameObject spellList;
    //ref to spell stack
    [SerializeField] private SpellStack spellStack;
    [SerializeField] private GameObject grid;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(spellList.GetComponent<SpellList>().spellList[0].spellName);
        spells[0]=spellList.GetComponent<SpellList>().spellList[0];
        spells[1]=spellList.GetComponent<SpellList>().spellList[1];
        spells[2]=spellList.GetComponent<SpellList>().spellList[2];
        //sets all slot icons to spells stored on launch if not blank
        for (int i = 0; i < spells.Length; i++)
        {
            if (spells[i] != null)
            {
                slots[i] = Instantiate(slotFab, grid.transform).GetComponent<SlotController>();
                slots[i].SetSpellInit(spells[i]);
            }
        }
    }

    void AddSpell(int slot)
    {
        //add spell to spellstack
        spellStack.addSpell(spells[slot]);
    }

    // Update is called once per frame
    void Update()
    {
        //temp measure, sub with player input (playerinput broken)
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
