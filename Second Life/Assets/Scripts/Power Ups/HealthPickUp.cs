/*
 * COMP3770 Final Project
 * HealthPickUp.cs
 * Coded by: Michelle Hua
 * Due: December 14, 2020
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    //Increases current and max health by 5
    public int addition = 5;
    private AudioSource collectableSound;

    public void Start()
    {
        collectableSound = GetComponent<AudioSource>();
    }

    //Calls pickup method if Player collides with object
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            collectableSound.Play();
            FindObjectOfType<PlayerController>().SetCovaCollected(FindObjectOfType<PlayerController>().GetCovaCollected() + 1);
            Pickup(other);
        }
    }

    void Pickup(Collider player)
    {
        //Displays debug message
        Debug.Log("Health collected");

        //Increase current and max health
        FindObjectOfType<PlayerController>().SetCurrentHealth(FindObjectOfType<PlayerController>().GetCurrentHealth() + addition);
        FindObjectOfType<PlayerController>().SetMaxHealth(FindObjectOfType<PlayerController>().GetMaxHealth() + addition);

        //Destorys object
        Destroy(gameObject);
    }
}
