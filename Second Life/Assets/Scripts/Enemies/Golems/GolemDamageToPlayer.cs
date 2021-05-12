/*
 * COMP3770 Final Project
 * GolemDamageToPlayer.cs
 * Coded By: Hannah Stam (103791045)
 * Due: December 14, 2020
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemDamageToPlayer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // If it collides with player, attacks player.
        if(other.gameObject.tag == "Player")
        {
            FindObjectOfType<PlayerController>().Attacked(FindObjectOfType<GolemController>().GetDamage());
        }
    }
}
