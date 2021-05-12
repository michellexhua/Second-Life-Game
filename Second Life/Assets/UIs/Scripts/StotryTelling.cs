using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StotryTelling : MonoBehaviour
{
    public TextMeshPro textBox1;
    public TextMeshProUGUI click;
    public Button exit;

    string m_Path;

    //Store all your text in this string array
    string[] goatText = new string[] {};

    int currentlyDisplayingText = 0;

    public string text { get; private set; }

    void Awake()
    {
        //Get the path of the Game data folder
        m_Path = Application.dataPath;
        TextAsset story = Resources.Load<TextAsset>("story");
        goatText = story.text.Split('\n');
        int i = 0;
        foreach (string element in goatText)
        {
            i++;
            Debug.Log(i + "th element: " + element);
        }
        //Output the Game data path to the console
        Debug.Log("dataPath : " + m_Path);
        Debug.Log("story : " + story.text);
        StartCoroutine(AnimateText());
    }
    //This is a function for a button you press to skip to the next text
    public void SkipToNextText()
    {   
        StopAllCoroutines();
        currentlyDisplayingText++;
        //If we've reached the end of the array, do anything you want. I just restart the example text
        if (currentlyDisplayingText > goatText.Length)
        {
            currentlyDisplayingText = 0;
            if (exit.interactable == false)
            {
                click.text = "Click to Exit"; 
                exit.interactable = true;
            }
           
        }
        else
        {
            StartCoroutine(AnimateText());
        }
        
    }

    private void destroy()
    {
        this.destroy();
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    //Note that the speed you want the typewriter effect to be going at is the yield waitforseconds (in my case it's 1 letter for every      0.03 seconds, replace this with a public float if you want to experiment with speed in from the editor)
    IEnumerator AnimateText()
    {
        if (currentlyDisplayingText < goatText.Length)
        {
            for (int i = 0; i < (goatText[currentlyDisplayingText].Length + 1); i++)
            {
                Debug.Log("i= " + i + "\nstring = goatText[" + currentlyDisplayingText + "]:" + goatText[currentlyDisplayingText]);
                textBox1.text = goatText[currentlyDisplayingText].Substring(0, i);
                yield return new WaitForSeconds(.02f);
            }
        }
            
    }

}
