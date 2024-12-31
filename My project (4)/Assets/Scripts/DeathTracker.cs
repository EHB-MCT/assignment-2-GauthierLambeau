using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTracker : MonoBehaviour
{
    public static Vector3 LastDeathPosition = Vector3.zero; 

    public void RecordDeathPosition(Vector3 position)
    {
        LastDeathPosition = position;
        Debug.Log("Death position recorded: " + position);
    }
}

