/*
 * COMP3770 Final Project
 * RedpinController.cs
 * Coded By: Hannah Stam (103791045)
 * Due: December 14, 2020
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedpinController : MonoBehaviour
{
    // Redpin Attributes
    private int currentHealth;
    private int maxHealth;
    private int damage;
    private AudioSource enemyHitSound;

    // Movement Variables
    public float speed;
    public GameObject player;
    private float waitTime;
    private float rotSpeed;
    private float walkStart;
    private float walkEnd;

    // Start is called before the first frame update
    void Start()
    {
        // Redpin Attributes
        damage = 45;
        maxHealth = 15;
        currentHealth = maxHealth;
        enemyHitSound = GetComponent<AudioSource>();

        //Movement Variables
        speed = 2.5f;
        waitTime = 1 * Time.deltaTime;
        rotSpeed = 1;
    }

    // Setter
    public void SetMaxHealth(int i) { maxHealth = i; }
    public void SetDamage(int i) { damage = i; }

    // Ensures that Redpin's health will not go below 0
    public void SetCurrentHealth(int i)
    {
        if (i <= 0) { i = 0; }
        this.currentHealth = i;
    }

    public void SetWalkStart(float walkStart)
    {
        this.walkStart = walkStart;
    }

    public void SetWalkEnd(float walkEnd)
    {
        this.walkEnd = walkEnd;
    }
    // Getter
    public int GetCurrentHealth() { return currentHealth; }
    public int GetMaxHealth() { return maxHealth; }
    public int GetDamage() { return damage; }

    public float GetWalkStart() { return walkStart; }
    public float GetWalkEnd() { return walkEnd; }

    // Checks if Redpin is dead
    public bool IsDead()
    {
        if (GetCurrentHealth() == 0) { return true; }
        else { return false; }
    }

    // Adjusts Redpin's health if it is attacked
    public void Attacked(int i)
    {
        enemyHitSound.Play();
        SetCurrentHealth(currentHealth - i);
        //Testing purposes
        Debug.Log("Redpin's health: " + currentHealth);
    }



    //If Redpin collides with an object that is not the player or the ground, it will wait for 1 second
    private void OnTriggerEnter(Collider other)
    {
        if(other.name != "Player" && other.name != "Ground")
        {
            StartCoroutine(Pause(other));
        }
    }

    IEnumerator Pause(Collider other)
    {
        yield return new WaitForSeconds(waitTime);
        StopCoroutine(Pause(other));
    }

    public void Update()
    {
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
        }
    }
}