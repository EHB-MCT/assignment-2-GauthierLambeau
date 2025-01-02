using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public string playerName;
    public float score;
    public int clickCount;

    public GameData(string playerName, float score, int clickCount)
    {
        this.playerName = playerName;
        this.score = score;
        this.clickCount = clickCount;
    }
}
