using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    public GameObject pauseMenuUI;  // Reference to the Pause Menu UI Canvas
    private bool isPaused = false;

    void Start()
    {
        // Ensure the pause menu is hidden at the start of the game
        pauseMenuUI.SetActive(false);
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);   
        Time.timeScale = 1f;            
        isPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);    
        Time.timeScale = 0f;            
        isPaused = true;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f; 
        isPaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }
    
    public void LoadMainMenu()
    {
        Time.timeScale = 1f; 
        isPaused = false;
        SceneManager.LoadScene("titleScene"); 
    }
}
