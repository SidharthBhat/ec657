using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{

    private bool gameIsPaused = false;
    private bool gameIsOver = false;
    public GameObject gameOverUI;
    public GameObject pauseMenuUI;
    public GameObject crosshair;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !gameIsOver)
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    //freezes gameplay
    void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        pauseMenuUI.SetActive(true);
        crosshair.SetActive(false);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    //goes to main menu scene
    public void MainMenuButton()
    {
        Save();
        pauseMenuUI.SetActive(false);
        crosshair.SetActive(true);
        Time.timeScale = 1f;
        gameIsPaused = false;
        SceneManager.LoadScene(0);
    }

    //make player start from beggining of the game
    public void Restart()
    {  
        Cursor.lockState = CursorLockMode.Locked;
        gameOverUI.SetActive(false);
        crosshair.SetActive(true);
        Time.timeScale = 1f;
        gameIsOver = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //displays gameOver when player dies
    public void GameOver()
    {  
        Cursor.lockState = CursorLockMode.None;
        crosshair.SetActive(false);
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsOver = true;
    }

    //resumes gameplay
    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenuUI.SetActive(false);
        crosshair.SetActive(true);
        Time.timeScale = 1f;
        gameIsPaused = false;

    }
    
    public void Save()
    {

    }

    public void QuitButton()
    {
        Save();
        Time.timeScale = 1f;
        Application.Quit();
    }


}
