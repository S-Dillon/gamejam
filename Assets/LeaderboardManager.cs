using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class LeaderboardManager : MonoBehaviour
{
    public TMP_Text timerText;  // Assign in the inspector
    public TMP_Text leaderboardText;  // Assign in the inspector

    private float timer = 0f;
    private bool isRunning = false;
    private List<LeaderboardEntry> leaderboard = new List<LeaderboardEntry>();

    private const int maxEntries = 5; // Show only top 3 runs
    private const string leaderboardKey = "leaderboard";

    void Start()
    {
        LoadLeaderboard();
        UpdateLeaderboardDisplay();
        StartTimer();
    }

    void Update()
    {
        if (isRunning)
        {
            timer += Time.deltaTime;
            timerText.text = FormatTime(timer);
        }
    }

    public void StartTimer()
    {
        timer = 0f;
        isRunning = true;
    }

    public void StopTimer()
    {
        isRunning = false;

        // Get saved username from PlayerPrefs
        string username = PlayerPrefs.GetString("PlayerUsername", "Player");

        SaveNewScore(username, timer);
    }

    private void SaveNewScore(string username, float time)
    {
        leaderboard.Add(new LeaderboardEntry(username, time));
        leaderboard = leaderboard.OrderBy(entry => entry.time).Take(maxEntries).ToList(); // Keep only top 3

        SaveLeaderboard();
        UpdateLeaderboardDisplay();
    }

    private void SaveLeaderboard()
    {
        // Convert leaderboard list to JSON string and store in PlayerPrefs
        LeaderboardData data = new LeaderboardData(leaderboard);
        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(leaderboardKey, json);
        PlayerPrefs.Save();
    }

    private void LoadLeaderboard()
    {
        if (PlayerPrefs.HasKey(leaderboardKey))
        {
            string json = PlayerPrefs.GetString(leaderboardKey);
            leaderboard = JsonUtility.FromJson<LeaderboardData>(json).entries;
        }
    }

    private void UpdateLeaderboardDisplay()
    {
        leaderboardText.text = "Leaderboard:\n";
        for (int i = 0; i < leaderboard.Count; i++)
        {
            leaderboardText.text += $"{i + 1}. {leaderboard[i].username} - {FormatTime(leaderboard[i].time)}\n";
        }

        // If there are no entries, display a placeholder message
        if (leaderboard.Count == 0)
        {
            leaderboardText.text += "No records yet!";
        }
    }

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        int milliseconds = Mathf.FloorToInt((time * 100) % 100);
        return $"{minutes:00}:{seconds:00}.{milliseconds:00}";
    }

    [System.Serializable]
    private class LeaderboardEntry
    {
        public string username;
        public float time;

        public LeaderboardEntry(string username, float time)
        {
            this.username = username;
            this.time = time;
        }
    }

    [System.Serializable]
    private class LeaderboardData
    {
        public List<LeaderboardEntry> entries;

        public LeaderboardData(List<LeaderboardEntry> entries)
        {
            this.entries = entries;
        }
    }
}
