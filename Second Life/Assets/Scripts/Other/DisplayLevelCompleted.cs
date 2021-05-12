using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayLevelCompleted : MonoBehaviour
{
    // Hides Level Complete canvas when game starts
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Shows Level Complete canvas when player dies
    public void ToggleLevelCompleted()
    {
        gameObject.SetActive(true);
    }
}
