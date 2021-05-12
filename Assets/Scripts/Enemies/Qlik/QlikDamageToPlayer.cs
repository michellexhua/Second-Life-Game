/*
 * COMP3770 Final Project
 * QlikDamageToPlayer.cs
 * Coded By: Hannah Stam (103791045)
 * Due: December 14, 2020
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QlikDamageToPlayer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // If it collides with player, attacks player.
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<PlayerController>().Attacked(FindObjectOfType<QlikController>().GetDamage());
        }

    }

}
