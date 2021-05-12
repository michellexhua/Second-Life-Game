using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DisplayBorad : MonoBehaviour
{
    public PlayerController player;
    public Slider healthBar;
    public TextMeshProUGUI[] listOfProperties;
    public TextMeshProUGUI[] listOfValues;
    TextMeshProUGUI collectableCountValue;
    TextMeshProUGUI onLevelValue;
    TextMeshProUGUI healthIndicator;
    public Vector3 offset;
    //public TextMeshProUGUI

    private int maxHealth;
    private int currentHealth;
    private int totalCollectedCount;

    // Start is called before the first frame update

    public void getPlayerPosition()
    {
        healthBar.transform.position = player.transform.position+offset;
        Debug.Log("getting health bar's position: " + healthBar.transform.position
            + "\ngetting player's position: "+ player.transform.position);
    }
    public void Start()
    {
        maxHealth = player.GetMaxHealth();
        currentHealth = player.GetCurrentHealth();
        totalCollectedCount = player.GetTotalCollected();
        healthBar.value = (1.0f * currentHealth) / maxHealth;

        Debug.Log("maxHealth = " + maxHealth + "\ncurrentHealth = " + currentHealth
            + "\ntotalCollectedCount = " + totalCollectedCount + "\nhealthBar.value = " + healthBar.value);


        foreach (TextMeshProUGUI textElement in listOfValues)
        {
            //tract all the adding elements
            //Debug.Log("TextMeshProUGUI listofValues = " + textElement.name + ". Its content =" + textElement.text);

            if (textElement.name == "value1")
            {
                collectableCountValue = textElement;
                collectableCountValue.text = "0";
            }
            if (textElement.name == "value2")
            {
                onLevelValue = textElement;
                onLevelValue.text = SceneManager.GetActiveScene().name;
            }
            if (textElement.name == "value3")
            {
                healthIndicator = textElement;
                healthIndicator.text = currentHealth.ToString();
            }

        }

    }

    // Update is called once per frame
    public void Update()
    {
      
        if (totalCollectedCount != player.GetTotalCollected()){
            totalCollectedCount = player.GetTotalCollected();
            collectableCountValue.text = totalCollectedCount.ToString();
          //  Debug.Log("maxHealth = " + maxHealth + "\ncurrentHealth = " + currentHealth
          //+ "\ntotalCollectedCount = " + totalCollectedCount + "\nhealthBar.value = " + healthBar.value);

        }
        if (currentHealth != player.GetCurrentHealth() || totalCollectedCount != player.GetTotalCollected())
        {
              Debug.Log("maxHealth = " + maxHealth + "\ncurrentHealth = " + currentHealth
            +"\ntotalCollectedCount = " + totalCollectedCount + "\nhealthBar.value = " + healthBar.value);
            healthIndicator.text = currentHealth.ToString();
            currentHealth = player.GetCurrentHealth();
            collectableCountValue.text = totalCollectedCount.ToString();
            totalCollectedCount = player.GetTotalCollected();
            healthBar.value = (1.0f * currentHealth) / maxHealth;
        }
    }
}
