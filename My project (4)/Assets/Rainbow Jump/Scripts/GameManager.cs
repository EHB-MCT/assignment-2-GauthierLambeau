using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ClickTracker clickTracker; // Link to the ClickTracker in the Inspector
    private int lastGameClicks = 0;

    public void StartGame()
    {
        if (clickTracker != null)
        {
            clickTracker.ResetClickCount();
        }
        Debug.Log("Game Started");
    }

    public void EndGame()
    {
        Debug.Log("Game Over");
        if (clickTracker != null)
        {
            lastGameClicks = clickTracker.GetLeftClickCount();
            clickTracker.EndGame(); // Stop counting clicks
        }
    }

    public int GetLastGameClicks()
    {
        return lastGameClicks;
    }
}
