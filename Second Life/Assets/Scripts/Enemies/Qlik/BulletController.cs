/*
 * COMP3770 Final Project
 * BulletController.cs
 * Coded By: Hannah Stam (103791045)
 * Due: December 14, 2020
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private int damage;

    public void Start()
    {
        damage = 50;
    }

    private void OnCollisionEnter(Collision other)
    {
        // If bullet hits the player, it damages the players health and self destructs
        if (other.collider.CompareTag("Player"))
        {
            FindObjectOfType<PlayerController>().Attacked(damage);
            Destroy(this.gameObject);
        }

    }

}
