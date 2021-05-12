/*
 * COMP3770 Final Project
 * DeathlingController.cs
 * Coded By: Hannah Stam (103791045)
 * Due: December 14, 2020
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathlingController : MonoBehaviour
{
    // Deathling Attributes
    private int currentHealth;
    private int maxHealth;
    private int damage;
    public GameObject player;
    private Vector3 playerPos;
    private AudioSource enemyHitSound;

    //Deathlings Movements
    private float speed;
    private float rotSpeed;
    private float walkStart;
    private float walkEnd;

    // Explosion Attributes
    private float radius;
    //private float power;
    //private float upforce;
    private float timer;
    private Vector3 explosionPos;
    public GameObject explosionPrefab;
    private GameObject newExplosion;
    private bool immune;


    // Start is called before the first frame update
    public void Start()
    {
        //Deathling Attributes
        damage = 45;
        maxHealth = 15;
        currentHealth = maxHealth;
        enemyHitSound = GetComponent<AudioSource>();

        // Movement Attributes
        speed = 1f;
        rotSpeed = 1f;

        // Explosion Attributes
        radius = 3f;
        //upforce = 6f;
        //upforce = 1f;
        //power = 1f;
        immune = false;
        timer = 0;

        StartCoroutine(Explode());
    }

    // Update is called once per frame
    void Update()
    {
        float playerX = player.transform.position.x;
        walkStart = GetWalkStart();
        walkEnd = GetWalkEnd();
        //Debug.Log("walkStart " + walkStart + " walkEnd " + walkEnd);
        if (walkStart < playerX && playerX < walkEnd)
        {
            // Start timer
            timer += Time.deltaTime;
            Debug.Log("Timer: " + timer);

            if (timer >= 0 && timer <= 1)
            {
                immune = true;
            }
            else if (timer > 1)
            {
                immune = false;
            }

            if (timer >= 5)
            {
                //Deathling moves beside the player every 5 seconds
                playerPos = player.transform.position;
                transform.position = new Vector3(playerPos.x - 1f, playerPos.y, playerPos.z);

                timer = 0;
            }

            // When the Deathling is not immune, it moves towards player
            if (!immune)
            {
                // Looks for player and moves towards players direction
                Vector3 direction = (player.transform.position - transform.position).normalized;
                Quaternion lookRot = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, rotSpeed * Time.deltaTime);
                transform.Translate(0, 0, speed * Time.deltaTime);
            }

            Destroy(newExplosion, 0.8f);
            // Checks if deathling needs to change colour
            ChangeColour();
        }
    }

    // Setter
    public void SetMaxHealth(int i) { maxHealth = i; }
    public void SetDamage(int i) { damage = i; }

    // Ensures that Deathling's health will not go below 0
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

    public float GetWalkStart() { return walkStart; }
    public float GetWalkEnd() { return walkEnd; }
    // Damage is adjusted depending how close the player is neart the explosion
    public int GetDamage()
    {
        explosionPos = newExplosion.transform.position;
        playerPos = player.transform.position;

        float d1 = radius * 0.25f;
        float d2 = radius * 0.5f;
        float d3 = radius * 0.75f;

        if(((explosionPos.x - playerPos.x) > 0 && (explosionPos.x - playerPos.x) <= d1) || ((explosionPos.y - playerPos.y) > 0 && (explosionPos.y - playerPos.y) <= d1) || ((explosionPos.z - playerPos.z) > 0 && (explosionPos.z - playerPos.z) <= d1))
        {
            damage = 5;
        }
        else if (((explosionPos.x - playerPos.x) > d1 && (explosionPos.x - playerPos.x) <= d2) || ((explosionPos.y - playerPos.y) > d1 && (explosionPos.y - playerPos.y) <= d2) || ((explosionPos.z - playerPos.z) > d1 && (explosionPos.z - playerPos.z) <= d2))
        {
            damage = 15;
        }
        else if (((explosionPos.x - playerPos.x) > d2 && (explosionPos.x - playerPos.x) <= d3) || ((explosionPos.y - playerPos.y) > d2 && (explosionPos.y - playerPos.y) <= d3) || ((explosionPos.z - playerPos.z) > d2 && (explosionPos.z - playerPos.z) <= d3))
        {
            damage = 30;
        }
        else if (((explosionPos.x - playerPos.x) > d3 && (explosionPos.x - playerPos.x) <= radius) || ((explosionPos.y - playerPos.y) > 0 && (explosionPos.y - playerPos.y) <= radius) || ((explosionPos.z - playerPos.z) > d3 && (explosionPos.z - playerPos.z) <= radius))
        {
            damage = 45;
        }
        else if (((explosionPos.x - playerPos.x) > radius) || ((explosionPos.y - playerPos.y) > radius) || ((explosionPos.z - playerPos.z) > radius))
        {
            damage = 0;
        }
        return damage;
    }
    public bool GetImmune() { return immune; }

    // Checks if Deathling is dead
    public bool IsDead()
    {
        if (GetCurrentHealth() == 0) { return true; }
        else { return false; }
    }

    // Adjusts Deathling's health if it is attacked
    public void Attacked(int i)
    {
        enemyHitSound.Play();
        SetCurrentHealth(currentHealth - i);
        //Testing purposes
        Debug.Log("Deathling's health: " + currentHealth);
    }

    // If Deathlin is Immune, becomes transparent
    public void ChangeColour()
    {
        if (immune)
        {
            gameObject.GetComponent<Renderer>().material.color = new Color32(0,0,0,65);
        }
        else
        {
            gameObject.GetComponent<Renderer>().material.color = new Color32(0, 0, 0, 255);
        }
    }

    // Explodes after 1 second
    IEnumerator Explode()
    {
        while (true)
        {
            // Waits for 1 second
            yield return new WaitForSeconds(1f);
            // Explodes
            newExplosion = Instantiate(explosionPrefab, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
            Destroy(newExplosion, 0.8f);
            // Waits for 5 seconds
            yield return new WaitForSeconds(5);

        }
    }

    // Damages Player
    private void OnTriggerEnter(Collider other)
    {
        
        //If it collides into player, it attacks player
        if (other.gameObject.tag == "Player")
        {
            int dam = GetDamage();
            FindObjectOfType<PlayerController>().Attacked(dam);
        }
    }
}
