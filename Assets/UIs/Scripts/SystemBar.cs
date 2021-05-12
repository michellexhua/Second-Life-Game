using UnityEngine;
using UnityEngine.UI;

public class SystemBar : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] audioClip;
    public Image image;

    private void Start()
    {
        OnApplicationResume();
        audioSource.Play();
        audioSource.volume = 0.75f;
    }

    public void OnApplicationResume()
    {
        Time.timeScale = 1;
        //adding this line if want the pause effect on music too
        //AudioListener.pause = false;
    }

    public void OnApplicationPause()
    {
        Time.timeScale = 0f;
        //adding this line if want the pause effect on music too
        //AudioListener.pause = false;
        //AudioListener.pause = true;
    }

    //concatinating parts,leave it for inGameUI
    public void setting()
    {

    }

    public void playNextMusic()
    {

        int index = System.Array.IndexOf(audioClip, audioSource.clip);

        //looping back from the end of the array
        if ((index + 1) >= audioClip.Length)
        {
            index = -1;
        }
        Debug.Log("index = "+ index);
        audioSource.clip = audioClip[index + 1];
        Debug.Log(audioSource.clip.name);
        audioSource.Play();

    }

    public void mute()
    {
        image.gameObject.SetActive(!image.gameObject.activeSelf);
        audioSource.mute = !audioSource.mute;
        Debug.Log("back ground music source mute : " + audioSource.mute);
    }

}
