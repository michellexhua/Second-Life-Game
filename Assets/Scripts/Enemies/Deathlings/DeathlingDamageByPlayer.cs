/*
 * COMP3770 Final Project
 * DeathlingDamageByPlayer.cs
 * Coded By: Hannah Stam (103791045)
 * Due: December 14, 2020
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathlingDamageByPlayer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //If it collides with player, it is attacked.
        if (other.name == "Player" && FindObjectOfType<DeathlingController>().GetImmune())
        {
            FindObjectOfType<DeathlingController>().Attacked(FindObjectOfType<PlayerController>().GetDamage());

            //If Deathling is dead, it destroys the entire Deathling object, setting the body collider as false
            if (FindObjectOfType<DeathlingController>().IsDead())
            {
                foreach (Transform child in transform)
                    child.gameObject.SetActive(false);
                Destroy(transform.parent.gameObject, 0.5f);
            }
        }
    }
}
