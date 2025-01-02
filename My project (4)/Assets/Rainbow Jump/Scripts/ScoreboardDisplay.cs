using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreboardDisplay : MonoBehaviour
{
    public ScoreboardManager scoreboardManager;
    public TextMeshProUGUI scoreboardText;

    void Start()
    {
        if (scoreboardManager != null)
        {
            scoreboardManager.LoadScoreboard();
            UpdateScoreboardUI();
        }
    }

    public void UpdateScoreboardUI()
    {
        if (scoreboardManager != null && scoreboardText != null)
        {
            string displayText = "Top 5 Runs:\n";

            foreach (var data in scoreboardManager.topScores)
            {
                // Format scores as integers and display
                displayText += $"{data.playerName} - Score: {Mathf.FloorToInt(data.score)}, Clicks: {data.clickCount}\n";
            }

            scoreboardText.text = displayText;
        }
    }
}
