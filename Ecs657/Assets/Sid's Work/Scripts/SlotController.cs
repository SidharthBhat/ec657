using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SlotController : MonoBehaviour
{
    //Slot is purely visual, the actual spells are stored in the hotbar
    private Image icon;
    [SerializeField] private Transform iconPrefab;
    private Spell slotSpell;

    // Start is called before the first frame update
    void Start()
    {
        icon = Instantiate(iconPrefab, transform).gameObject.GetComponent<Image>();
        SetSpell(slotSpell);
    }

    public string debug()
    {
        return slotSpell.spellName;
    }

    public void SetSpell(Spell spell)
    {
        slotSpell = spell;
        icon.sprite=spell.icon;
        icon.enabled=true;
    }

    //seems strange to have these two, but it's because adding the spell right after instantiating, Start is still running
    //so this only assigns the spell, then the slot runs the set itself
    //doing it after instantiation requires setspel
    //because instantiate doesn't allow parameters/constructors for some reason
    public void SetSpellInit(Spell spell)
    {
        slotSpell = spell;
    }

    public void clearSlot()
    {
        icon.sprite=null;
        icon.enabled = false;
    }
}
