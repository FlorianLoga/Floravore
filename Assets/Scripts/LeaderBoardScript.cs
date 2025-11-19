using System.Collections.Generic;
using System.IO;
using UnityEngine;



public class LeaderboardScript : MonoBehaviour
{
    public static LeaderboardScript Instance;

    private string filePath;
    public ScoreList leaderboard = new ScoreList();

    public GameObject scoreEntryPrefab;
    public Transform entryContainer;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            filePath = Application.persistentDataPath + "/leaderboard.json";
            LoadLeaderboard();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(string name, float time)
    {
        leaderboard.scores.Add(new ScoreEntry { playerName = name, timeSurvived = time });
        leaderboard.scores.Sort((a, b) => b.timeSurvived.CompareTo(a.timeSurvived)); 
        SaveLeaderboard();
    }

    void SaveLeaderboard()
    {
        string json = JsonUtility.ToJson(leaderboard, true);
        File.WriteAllText(filePath, json);
    }

    void LoadLeaderboard()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            Debug.Log("Loaded leaderboard: " + json);
            leaderboard = JsonUtility.FromJson<ScoreList>(json);
        }
    }

    public List<ScoreEntry> GetTopScores(int count = 5)
    {
        return leaderboard.scores.GetRange(0, Mathf.Min(count, leaderboard.scores.Count));
    }

    public void DisplayScores()
    {   
        foreach (Transform child in entryContainer)
            Destroy(child.gameObject); 

        foreach (var score in GetTopScores())
        {
            GameObject entry = Instantiate(scoreEntryPrefab, entryContainer);
            var text = entry.GetComponent<TMPro.TextMeshProUGUI>();
            text.text = $"{score.playerName} - {score.timeSurvived:F2}s";
        }
    }
}
