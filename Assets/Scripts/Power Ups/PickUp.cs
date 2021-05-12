/*
 * COMP3770 Final Project
 * PickUp.cs
 * Coded by: Michelle Hua
 * Due: December 14, 2020
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Powerup object gets destroyed when player collides
public class PickUp : MonoBehaviour
{
    private AudioSource powerupSound;

    public void Start()
    {
        powerupSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            powerupSound.Play();
            Pickup();
        }
    }

    void Pickup()
    {
        Debug.Log("Power up collected");

        Destroy(gameObject);
    }
}
