using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour
{
    public void StartGame()
    {
        
        if (GameController.instance != null)
        {
            GameController.instance.StartGame();
        }
    }

    public void LoadLeaderboard()
    {
        if (GameController.instance != null)
        {
            GameController.instance.loadLeaderboard();
        }
    }

    public void ExitGame()
    {
        
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; 
        #endif
    }
}
