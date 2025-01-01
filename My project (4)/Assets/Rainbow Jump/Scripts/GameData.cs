using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public float score;
    public int clickCount;

    public GameData(float score, int clickCount)
    {
        this.score = score;
        this.clickCount = clickCount;
    }
}
