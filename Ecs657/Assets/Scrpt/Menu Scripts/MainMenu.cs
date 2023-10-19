using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    const String  stage = "GameStage";
    public LoadingScreen loadingScreen; 
    [SerializeField] GameObject continueButton;
    void Start()
    {
        int gameInfo = PlayerPrefs.GetInt(stage,0);
        if (gameInfo == 0)
        {
            continueButton.SetActive(false);
        }
        else
        {
            continueButton.SetActive(true);
        }
    }

    public void NewGame()
    {
        loadingScreen.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void ContinueGame()
    {
        loadingScreen.LoadLevel(SceneManager.GetActiveScene().buildIndex + 2);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
