using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelComplete : MonoBehaviour
{
    public DisplayLevelCompleted displayLevelCompleted;

    //If player touches deathzone, gameover display displays
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            displayLevelCompleted.ToggleLevelCompleted();
        }
    }
}
