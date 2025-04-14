using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class ScoreEntry
{
    public string playerName;
    public float score; 
}

[System.Serializable]
public class SerializableScoreList
{
    public List<ScoreEntry> scores;
}

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    private List<ScoreEntry> scores = new List<ScoreEntry>();
    private string saveFilePath;
    public int maxScores = 5;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); 
            saveFilePath = Path.Combine(Application.persistentDataPath, "leaderboard.dat");
            LoadScores();
        }
        else
        {
            Destroy(gameObject); // Ensure only one instance exists
        }
    }

    public bool IsHighScore(float score){
        if (scores.Count < maxScores) return true;
        return score < scores.Last().score;
    }

    public void AddScore(string playerName, float score)
    {
        ScoreEntry newScore = new ScoreEntry { playerName = playerName, score = score };
        scores.Add(newScore);
        scores = scores.OrderBy(s => s.score).Take(maxScores).ToList();
        SaveScores();
    }

    public List<ScoreEntry> GetTopScores()
    {
        return new List<ScoreEntry>(scores);
    }

    private void SaveScores()
    {
        try
        {
            SerializableScoreList saveData = new SerializableScoreList { scores = scores };
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(saveFilePath, FileMode.Create))
            {
                formatter.Serialize(stream, saveData);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error saving scores: {e.Message}");
        }
    }

    private void LoadScores()
    {
        if (File.Exists(saveFilePath))
        {
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream stream = new FileStream(saveFilePath, FileMode.Open))
                {
                    SerializableScoreList saveData = (SerializableScoreList)formatter.Deserialize(stream);
                    scores = saveData.scores;
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Error loading scores: {e.Message}");
                scores = new List<ScoreEntry>();
            }
        }
    }

}
