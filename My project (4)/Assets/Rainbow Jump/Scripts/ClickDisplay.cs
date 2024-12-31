using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class ClickDisplay : MonoBehaviour
{
    public GameManager gameManager;
    public TextMeshProUGUI clickCountText; 

    void Start()
    {
        if (gameManager != null && clickCountText != null)
        {
            int lastGameClicks = gameManager.GetLastGameClicks();
            clickCountText.text = "Last Game Clicks: " + lastGameClicks;
        }
    }
}
