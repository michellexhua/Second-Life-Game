/*
 * COMP3770 Final Project
 * RedpinDamageToPlayer.cs
 * Coded By: Hannah Stam (103791045)
 * Due: December 14, 2020
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedpinDamageToPlayer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //If it collides into player, it attacks player
        if (other.gameObject.tag == "Player")
        {
            FindObjectOfType<PlayerController>().Attacked(FindObjectOfType<RedpinController>().GetDamage());
        }
    }
}
