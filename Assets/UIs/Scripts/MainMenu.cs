
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //MonoBehaviour monoBehaviour;
    private GameBench.SceneLoader sceneLoader;

    private void Start()
    {
        //GameBench.SceneLoader.Instantiate(monoBehaviour, new Vector3(0, 0, 0), Quaternion.identity);
       
    }

    public void loadStory()
   {
        //sceneLoader.LoadingInterface("story");
        SceneManager.LoadScene("story");
        
   }

   public void QuitGame()
   {
        
       Application.Quit();
        Debug.Log("Quit() is called!");
   }
}
