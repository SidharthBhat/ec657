using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private Menus menus;
    #region hpVariables
    public int maxHitPoints;
    private int hitPoints;
    public int hpIncreasePerLevel;
	#endregion

	#region xpVariables
    [SerializeField] private float experienceTillNextLevel;
    [SerializeField] private float experienceNeededMultiplier;
    private float currentExperience;
    public int level;
    #endregion

    #region uiVariables
    [SerializeField] private TMP_Text LevelUI;
    [SerializeField] private HealthBar healthbar; 
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

    // Increase hp by x amount 
    public void Heal(int amount)
    {
        hitPoints += amount;
        //if healed over max hp, clamp to max hp
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
            maxHitPoints += hpIncreasePerLevel;
            healthbar.setMaxHealth(maxHitPoints);
            currentExperience -= experienceTillNextLevel;
            experienceTillNextLevel *= experienceNeededMultiplier;
            level++;
		}
        LevelUI.text = "Level " + level + ": (" + Mathf.Round(currentExperience) + "/" + Mathf.Round(experienceTillNextLevel) + ")"; 
	}

	#endregion
	//------------------------------------------------------------------//
}
