using UnityEngine;
using UnityEngine.UI;

public class playerDeathReact : MonoBehaviour
{
    public PlayerController playerController;
    public int totalCollectable { get; set; }
    public int currentHealth { get; set; }
    private int maxHealth;
    public void SetMaxHealth(int maxHealth) { this.maxHealth = maxHealth; }
    public int GetMaxHealth() { return maxHealth; }

    public playerDeathReact(int totalCollectable)
    {
        this.totalCollectable = totalCollectable;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = playerController.GetCurrentHealth();
        totalCollectable = playerController.GetCovaCollected() + playerController.GetTPPowerupCollected();
        SetMaxHealth(playerController.GetMaxHealth());
    }

    // get player stats in real time
    void Update()
    {
        int count = playerController.GetCovaCollected() + playerController.GetTPPowerupCollected();
        if (count != totalCollectable)
        {
            totalCollectable = count;
        }
        if (currentHealth != playerController.GetCurrentHealth())
        {
            currentHealth = playerController.GetCurrentHealth();
        }

    }
}
