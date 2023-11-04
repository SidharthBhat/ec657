using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    #region uiVariables
    [SerializeField] TMP_Text LevelUI;
    [SerializeField] HealthBar healthbar; 
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        hitPoints = maxHitPoints;
        level = 0;
        healthbar.setMaxHealth(maxHitPoints);
        LevelUI.text = "Level " + level + ": (" + Mathf.Round(currentExperience) + "/" + Mathf.Round(experienceTillNextLevel) + ")";
    }
    //------------------------------------------------------------------//
	#region hpCode
	// Update is called once per frame
	public void TakeDamage(int amount)
    {
        hitPoints -= amount;
        healthbar.setHealth(hitPoints);
        if (hitPoints <= 0)
        {
            menus.GameOver();
        }   
    }

    // Sets health to be its max possible value
    public void Heal(int amount)
    {
        hitPoints += amount;
        if (hitPoints >= maxHitPoints)
        {
            hitPoints = maxHitPoints;
        }
        healthbar.setHealth(hitPoints);
    }
    #endregion;
    //------------------------------------------------------------------//
    #region xpCode
    public void AddXp(float value)
	{
        currentExperience += value;
        while(currentExperience >= experienceTillNextLevel)
		{
            currentExperience -= experienceTillNextLevel;
            experienceTillNextLevel *= xpNeededMultiplier;
            level++;
		}
        LevelUI.text = "Level " + level + ": (" + Mathf.Round(currentExperience) + "/" + Mathf.Round(experienceTillNextLevel) + ")"; 
	}
	#endregion
	//------------------------------------------------------------------//
}
