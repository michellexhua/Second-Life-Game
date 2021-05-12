using UnityEngine;
using UnityEngine.SceneManagement;


public class GameSelection : MonoBehaviour
{
    public void loadLevel1()
    {
        SceneManager.LoadScene("level1");
    }

    public void loadLevel2()
    {
        SceneManager.LoadScene("level2");
    }

    public void loadLevel3()
    {
        SceneManager.LoadScene("level3");
    }

    public void loadLevelGenTest()
    {
        SceneManager.LoadScene("LevelGenTest");
    }
}
