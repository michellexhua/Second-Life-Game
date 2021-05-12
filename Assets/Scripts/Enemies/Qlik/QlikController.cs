/*
 * COMP3770 Final Project
 * QlikController.cs
 * Coded By: Hannah Stam (103791045)
 * Due: December 14, 2020
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QlikController : MonoBehaviour
{
    // Qlik attributes
    private int currentHealth;
    private int maxHealth;
    private int damage;
    private AudioSource enemyHitSound;

    // Movement & Projectile Attributes
    public GameObject player;
    public GameObject bullet;
    private GameObject newBullet;
    private Rigidbody newBulletRB;
    private float bulForce;
    public float speed;
    private float rotSpeed;
    public float timer;
    private bool shoot;
    private float shootTime; // Time Qlike will shoot projectiles (5 sec as specified by prof)
    private float walkTime; // Time inbetween when the Qlik will walk around after throwing projectiles
    private float maxTime; // total time Qlik will throw projectiles and walk around
    private float waitTime; // Time that the Qlik waits to attack and to throw projectiles at the player
    private float walkStart;
    private float walkEnd;

    // Start is called before the first frame update
    private void Start()
    {
        // Qlik Attributes
        maxHealth = 15;
        damage = 25;
        currentHealth = maxHealth;
        enemyHitSound = GetComponent<AudioSource>();

        // Movement and Projectile Attributes
        speed = 1.5f;
        rotSpeed = 5f;
        timer = 0;
        bulForce = 10f;
        shoot = false;
        waitTime = 1.5f;
        walkTime = 5f;
        shootTime = 5f;
        maxTime = shootTime + walkTime;

        StartCoroutine(ShootAtPlayer(waitTime));
    }

    // Setter
    public void SetMaxHealth(int i) { maxHealth = i; }
    public void SetDamage(int i) { damage = i; }

    // Ensures that Qlik's health will not go below 0
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
    // Checks if Qlik is dead
    public bool IsDead()
    {
        if (GetCurrentHealth() == 0) { return true; }
        else { return false; }
    }

    // Adjusts Qlik's health if it is attacked
    public void Attacked(int i)
    {
        enemyHitSound.Play();
        SetCurrentHealth(currentHealth - i);
        //Testing purposes
        Debug.Log("Qlik's health: " + currentHealth);
    }

    // Update is called once per frame
    public void Update()
    {
        // Qlik Moves Towards the player
        float playerX = player.transform.position.x;
        walkStart = GetWalkStart();
        walkEnd = GetWalkEnd();
        if (walkStart < playerX && playerX < walkEnd)
        {
            //  Starts timer
            timer += Time.deltaTime;

            //  When the timer is between 0 & shoot time, Qlik shoots projectiles  
            if (timer >= 0 && timer <= shootTime)
            {
                shoot = true;
            }
            else if (timer > shootTime)
            {
                // Qlik stops shooting when timer is past the shoot time
                shoot = false;
            }
            else if (timer >= maxTime)
            {
                //  When max time is reached, timer goes back down to 0
                timer = 0;
            }

            Vector3 direction = (player.transform.position - transform.position).normalized;
            Quaternion lookRot = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, rotSpeed * Time.deltaTime);
            transform.Translate(0, 0, speed * Time.deltaTime);

            // Destroys bullets after 2.5 seconds
            Destroy(newBullet, 2.5f);
        }

    }
    
    // Qlik shoots projectiles
    IEnumerator ShootAtPlayer(float time)
    {
        while (true)
        {
            if (shoot)
            {
                //Shoots the bullet
                newBullet = Instantiate(bullet, (gameObject.transform.position + gameObject.transform.forward), gameObject.transform.rotation) as GameObject;
                newBulletRB = newBullet.GetComponent<Rigidbody>();
                newBulletRB.AddForce(transform.forward * bulForce, ForceMode.Impulse);
                yield return new WaitForSeconds(time);               
            }
            // waits for time seconds
            yield return new WaitForSeconds(time);
        }                
    }
}