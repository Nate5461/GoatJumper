using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    private StopWatch stopWatch;
    public float currentScore;

    private void Awake()
{
    if (instance == null)
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    else
    {
        Destroy(gameObject);
    }


}


    public void StartGame()
    {
        SceneManager.LoadScene("mainScene");
    }

    public void loadLeaderboard()
    {
        SceneManager.LoadScene("LoadLeaderboard");
    }

    public void EndGame()
{
    if (stopWatch == null)
    {
        stopWatch = FindObjectOfType<StopWatch>(); // Try finding StopWatch again
    }

    if (stopWatch != null)
    {
        StopStopwatch();
        PlayerPrefs.SetFloat("ElapsedTime", stopWatch.GetElapsedTime());
    }
    else
    {
        Debug.LogError("StopWatch is not initialized in EndGame!");
    }

    SceneManager.LoadScene("Save Score");
}

    public float GetElapsedTime()
    {
        return currentScore = stopWatch.GetElapsedTime();
    }

    private void StopStopwatch()
    {
        if (stopWatch != null)
        {
            stopWatch.StopStopwatch(); // Stop the stopwatch
        }
    }

    
}
