using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickTracker : MonoBehaviour
{
    private int leftClickCount = 0;
    private bool isGameActive = false;

    void Start()
    {
        ResetClickCount();
        isGameActive = true; // Game starts active
    }

    void Update()
    {
        if (isGameActive && Input.GetMouseButtonDown(0))
        {
            leftClickCount++;
            Debug.Log("Left Click Count: " + leftClickCount);
        }
    }

    public int GetLeftClickCount()
    {
        return leftClickCount;
    }

    public void EndGame()
    {
        isGameActive = false; // Stop tracking clicks
    }

    public void ResetClickCount()
    {
        leftClickCount = 0; // Reset the counter
        isGameActive = true; // Reactivate click tracking
    }
}
