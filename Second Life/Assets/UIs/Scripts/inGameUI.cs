using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class inGameUI : MonoBehaviour
{
    public Button buttonPanel;
    public Button soundPanel;
    public Button soundButton;
    //public AudioMixer mixer;
    public Slider Soundslider;
    public TextMeshProUGUI soundValueDisplay;
    public AudioSource audioSource;

    public inGameUI(AudioSource audioSource) => this.audioSource = audioSource;

    private float previousVolume = 75f;

    public void Start()
    {
        //Time.timeScale = 1;
        Soundslider.value = PlayerPrefs.GetFloat("MusicVolume", previousVolume);
        audioSource.volume = Soundslider.value;
        soundValueDisplay.text = ((int)(Soundslider.value * 100)).ToString();

    }

    public void Awake()
    {
        buttonPanel.image.gameObject.SetActive(false);
        soundPanel.image.gameObject.SetActive(false);
        soundButton.image.gameObject.SetActive(false);
    }

    public void PreviousVolume() => previousVolume = audioSource.volume;

    public void updateSound()
    {
        audioSource.volume = Soundslider.value;
        soundValueDisplay.text = ((int)(Soundslider.value * 100)).ToString();
        Debug.Log("call method : " + this.name);
    }

    public void SoundSettingCancel()
    {
        audioSource.volume = previousVolume;
        Soundslider.value = audioSource.volume;
        soundValueDisplay.text = ((int)(previousVolume * 100)).ToString();
        Debug.Log("call method : " + this.name);
    }

    public void SetLevel(Slider slider)
    {
        float sliderValue = slider.value;
        PlayerPrefs.SetFloat("MusicVolume", sliderValue);
        soundValueDisplay.text = ((int)(sliderValue * 100)).ToString();
        audioSource.volume = sliderValue;
        Debug.Log(" slider value: " + sliderValue);
    }

    public void showboard(GameObject buttonPanel)
    {
        bool isActive = buttonPanel.activeSelf;
        buttonPanel.SetActive(!isActive);
        Debug.Log("call method : " + this.name);
    }

    public void loadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Debug.Log("call method : " + this.name);
    }

    public void replay()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        Debug.Log("Current scene name: " + currentScene);
        //SceneManager.sceneUnloaded(currentScene);
        SceneManager.LoadScene(currentScene);
        Debug.Log("call method : " + this.name);
    }

    public void OnApplicationQuit()
    {
        Application.Quit();
        Debug.Log("call method : " + this.name);
    }

}
