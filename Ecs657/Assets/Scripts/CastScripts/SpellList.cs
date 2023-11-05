using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellList : MonoBehaviour
{
    /* Explanation of purpose:
     * Because of how Unity works, actual OOP doesn't really work, that's where this comes in
     * The SpellList object houses instances of the spells as components, and those are stored in this components to be retrieved by the other objects
     * It also means spells for the hotbar and for combinations are possible here
     */
    public Spell[] spellList;
    public Spell[] hotbarList;

}
