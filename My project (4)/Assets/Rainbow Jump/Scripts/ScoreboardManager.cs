using System.Collections;
using System.Collections.Generic;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ScoreboardManager : MonoBehaviour
{
    private const string scoreboardFileName = "Scoreboard.json";

    public List<GameData> topScores = new List<GameData>();

    private string GetScoreboardPath()
    {
        return Path.Combine(Application.persistentDataPath, scoreboardFileName);
    }

    public void LoadScoreboard()
    {
        string filePath = GetScoreboardPath();

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            topScores = JsonUtility.FromJson<ScoreboardData>(json)?.gameDataList ?? new List<GameData>();
        }
        else
        {
            topScores = new List<GameData>();
        }
    }

    public void SaveScoreboard()
    {
        string json = JsonUtility.ToJson(new ScoreboardData(topScores), true);
        File.WriteAllText(GetScoreboardPath(), json);
    }

    public void AddGameData(GameData data)
    {
        topScores.Add(data);

        // Sort by score in descending order
        topScores.Sort((a, b) => b.score.CompareTo(a.score));

        // Keep only the top 5
        if (topScores.Count > 5)
        {
            topScores = topScores.GetRange(0, 5);
        }

        SaveScoreboard();
    }
}

[System.Serializable]
public class ScoreboardData
{
    public List<GameData> gameDataList;

    public ScoreboardData(List<GameData> gameDataList)
    {
        this.gameDataList = gameDataList;
    }
}
