using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] int maxHitPoints;
    [SerializeField] Menus menus;
    int hitPoints;

    // Start is called before the first frame update
    void Start()
    {
       hitPoints = maxHitPoints; 
    }

    // Update is called once per frame
    public void TakeDamage(int amount)
    {
        hitPoints -= amount;
        if (hitPoints < 0)
        {
            menus.GameOver();
        }   
    }

    public void Heal(int amount)
    {
        hitPoints += amount;
        if (hitPoints > maxHitPoints)
        {
            hitPoints = maxHitPoints;
        }
    }

}
