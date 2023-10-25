using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TODO : MonoBehaviour
{
    /*Just to refresh, hotbar is going funky, and the spell doesn't cast
     * and spellstack is fucked too
     * However, icon and sprite work fine now, because it required UI, not UIElements for the import
     * Now hotbar's been refactored to instantiate the slots in grid, and make slots fully private
     * And now spellstack been refactored to instantiate its own slots, too
     * Additionally, spell stacking's been reworked to account for order and position, allowing for compound stacking
     * Like stacking line clears in tetris
     * 
     * After that, refactor both to search for spell list and take the actual spell list from it
     * Allowing spellList to exist outside the prefab, allowing for whatever spell loadouts to begin
     * Check that fire works after, and it's smooth sailing from there with the new spells
     */
}
