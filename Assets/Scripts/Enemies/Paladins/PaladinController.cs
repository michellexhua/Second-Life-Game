/*
 * COMP3770 Final Project
 * PaladinController.cs
 * Coded By: Hannah Stam (103791045)
 * Due: December 14, 2020
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PaladinController : MonoBehaviour
{
    // Paladin Attributes
    private int currentHealth;
    private int maxHealth;
    private int damage;
    private AudioSource enemyHitSound;

    // Movement Variables
    public GameObject player;
    public float speed;
    private float rotSpeed;
    private float walkStart;
    private float walkEnd;

    private float timer;
    // private float impact;
    

    // Start is called before the first frame update
    void Start()
    {
        // Paladin Attributes
        damage = 100;
        maxHealth = 75;
        currentHealth = maxHealth;
        enemyHitSound = GetComponent<AudioSource>();

        //Movement Variables
        speed = 0.75f;
        rotSpeed = 1;

        timer = 0;
        // impact = 400f;
    }

    // Setter
    public void SetMaxHealth(int i) { maxHealth = i; }
    public void SetDamage(int i) { damage = i; }

    public void SetWalkStart(float walkStart)
    {
        this.walkStart = walkStart;
    }

    public void SetWalkEnd(float walkEnd)
    {
        this.walkEnd = walkEnd;
    }
    // Ensures that Golem's health will not go below 0
    public void SetCurrentHealth(int i)
    {
        if (i <= 0) { i = 0; }
        this.currentHealth = i;
    }

    // Getter
    public int GetCurrentHealth() { return currentHealth; }
    public int GetMaxHealth() { return maxHealth; }
    public int GetDamage() { return damage; }

    public float GetWalkStart() { return walkStart; }
    public float GetWalkEnd() { return walkEnd; }
    // Checks if Golem is dead
    public bool IsDead()
    {
        if (GetCurrentHealth() == 0) { return true; }
        else { return false; }
    }

    // Adjusts Golem's health if it is attacked
    public void Attacked(int i)
    {
        enemyHitSound.Play();
        SetCurrentHealth(currentHealth - i);
        //Testing purposes
        Debug.Log("Paladin's health: " + currentHealth);
    }

    public void Update()
    {
        // Looks for player and moves towards players direction
        // Following the player
        float playerX = player.transform.position.x;
        walkStart = GetWalkStart();
        walkEnd = GetWalkEnd();
        if (walkStart < playerX && playerX < walkEnd)
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            Quaternion lookRot = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, rotSpeed * Time.deltaTime);
            transform.Translate(0, 0, speed * Time.deltaTime);

            timer += 1 * Time.deltaTime;

            if (timer >= 5)
            {
                Jump();
                timer = 0;
            }
        }
    }

    public void Jump()
    {
        Vector3 pos = player.transform.position;

        transform.position = new Vector3(pos.x - 2, pos.y, pos.z);
    }
}
