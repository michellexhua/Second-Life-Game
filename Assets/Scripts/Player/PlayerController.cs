/*
 * COMP3770 Final Project
 * PlayerController.cs
 * Coded By: Hannah Stam (103791045)
 * Due: December 14, 2020
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public DisplayGameOver displayGameOver;

    //Player Attributes
    public int currentHealth;
    public int maxHealth;
    private int damage;
    private int covaCollected;
    private int TPPowerupCollected;
    private int totalCollected;
    private AudioSource playerHitSound;

    //Camera Check attributes
    private float minX;
    private bool inScene;

    //Movement Variables
    private Rigidbody rb;
    private CapsuleCollider col;
    private float speed;
    private float run;
    private float jumpForce;
    private bool ground;
    private bool doubleJump;
    private bool leftCtrl;
    private bool rightCtrl;

    //change Start() to Awake() avoid UI initializating errors.
    private void Awake()
    {
        //Player attributes
        currentHealth = maxHealth;
        damage = 15;
        covaCollected = 0;
        TPPowerupCollected = 0;
        totalCollected = 0;
        playerHitSound = GetComponent<AudioSource>();

        //Camera Check Attributes
        minX = -15f;
        inScene = true;

        //Movement Variables
        rb = GetComponent<Rigidbody>();
        col = this.GetComponent<CapsuleCollider>();
        speed = 3.0f;
        run = 2.5f;
        jumpForce = 15f;
        ground = true;
        doubleJump = false;
        leftCtrl = false;
        rightCtrl = false;
    }
    public void SetMinX(float newX)
    {
        minX = newX;
    }
    // Setter
    public void SetMaxHealth(int i) { maxHealth = i; }
    public void SetDamage(int i) { damage = i; }
    public void SetCovaCollected(int i) { covaCollected = i; }
    public void SetTPPowerupCollected(int i) { TPPowerupCollected = i;  }

    //Sets Players currentHealth and ensures it does not go below 0
    public void SetCurrentHealth(int i)
    {
        if (i <= 0) { i = 0; }
        if (i >= maxHealth && maxHealth > 0) { i = this.maxHealth; }
        this.currentHealth = i;
    }

    // Getter
    public int GetCurrentHealth() { return currentHealth; }
    public int GetMaxHealth() { return maxHealth; }
    public int GetDamage() { return damage; }
    public int GetCovaCollected() { return covaCollected; }
    public int GetTPPowerupCollected() { return TPPowerupCollected; }
    public bool GetDoubleJump() { return doubleJump; }
    public bool GetGround() { return ground; }
    public int GetTotalCollected() { return totalCollected; }
    public float GetMinX() { return minX; }

    public void CollectCova()
    {
        SetCovaCollected(covaCollected++);
    }

    public void CollectTPP()
    {
        SetTPPowerupCollected(TPPowerupCollected++);
    }

    // Checks if Player is dead
    public bool IsDead()
    {
        if (GetCurrentHealth() == 0) { return true; }
        else { return false; }
    }

    // Adjusts Player's health if they are attacked
    public void Attacked(int i)
    {
        playerHitSound.Play();
        SetCurrentHealth(GetCurrentHealth() - i);

        //Testing purposes
        Debug.Log("Player's health: " + currentHealth);
    }

    // Used when character collides with collectables and powerups
    public void Healed(int i)
    {
        SetCurrentHealth(currentHealth + i);
    }

    // Update is called once per frame
    void Update()
    {
        //PLAYER MOVEMENT
        // Checks if left control key is pressed
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            leftCtrl = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            leftCtrl = false;
        }
        // Checks if right control key is pressed
        if (Input.GetKeyDown(KeyCode.RightControl))
        {
            rightCtrl = true;
        }
        if (Input.GetKeyUp(KeyCode.RightControl))
        {
            rightCtrl = false;
        }

        // Checks if player is within the camera's range
        if(transform.position.x <= GetMinX())
        {
            inScene = false;

        }
        else
        {
            inScene = true;
        }


        // Player walks if control button is not pressed
        if (!leftCtrl && !rightCtrl) //add the inScene  if it works
        {
            transform.Translate(speed * Input.GetAxis("Horizontal") * Time.deltaTime, 0f, 0f);
            
        }
        else { /* player does not move */ }
        // Player runs if control button is pressed
        if (leftCtrl || rightCtrl)  //add inScene if it works
        {
            transform.Translate(run * speed * Input.GetAxis("Horizontal") * Time.deltaTime, 0f, 0);
        }
        else { /*player does not move */ }

        // Checks if player is on the ground
        RaycastHit hit;
        Vector3 physicsCentre = this.transform.position + this.col.center;
        if (Physics.Raycast(physicsCentre, Vector3.down, out hit, 1.2f))
        {
            if (hit.transform.gameObject.tag != "Player")
            {
                ground = true;
            }
        }
        else
        {
            ground = false;
        }

        // Player does a double jump if the player is not on the ground
        if (Input.GetKeyDown(KeyCode.Space) && !ground && doubleJump)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            doubleJump = false;
        }
        // Player jumps if the player is on the ground
        else if (Input.GetKeyDown(KeyCode.Space) && ground)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            doubleJump = true;
        }
    }

    public void FixedUpdate()
    {
        if (IsDead())
        {
            displayGameOver.ToggleGameOver();
        }
    }
}

