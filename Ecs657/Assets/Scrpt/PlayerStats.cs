using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] Menus menus;
    #region hpVariables
    [SerializeField] int maxHitPoints;
    int hitPoints;
	#endregion
	#region xpVariables
    [SerializeField] public float experienceTillNextLevel;
    [SerializeField] public float currentExperience;
    [SerializeField] public int level;
    [SerializeField] public float xpNeededMultiplier;
	#endregion

	// Start is called before the first frame update
	void Start()
    {
        hitPoints = maxHitPoints;
        level = 0;
    }
    //------------------------------------------------------------------//
	#region hpCode
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
    #endregion;
    //------------------------------------------------------------------//
    #region xpCode
    public void AddXp(float value)
	{
        currentExperience += value;
        while(currentExperience > experienceTillNextLevel)
		{
            currentExperience -= experienceTillNextLevel;
            experienceTillNextLevel *= xpNeededMultiplier;
            level++;
		}
	}
	#endregion
}
