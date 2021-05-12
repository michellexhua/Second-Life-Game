using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deathzone : MonoBehaviour
{
    public DisplayGameOver displayGameOver;

    //If player touches deathzone, gameover display displays
    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            displayGameOver.ToggleGameOver();
        }
    }
}