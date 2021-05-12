using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayGameOver : MonoBehaviour
{
    // Hides GameOver canvas when game starts
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Shows GameOver canvas when player dies
    public void ToggleGameOver()
    {
        gameObject.SetActive(true);
    }
}
