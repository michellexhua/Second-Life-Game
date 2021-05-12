using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToMenu : MonoBehaviour
{
    public void MainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
