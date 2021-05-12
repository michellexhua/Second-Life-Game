/*
 * COMP3770 Final Project
 * DeathlingDamageToPlayer.cs
 * Coded By: Hannah Stam (103791045)
 * Due: December 14, 2020
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathlingDamageToPlayer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //If it collides into player, it attacks player
        try
        {
            if (other.gameObject.tag == "Player")
            {
                if (other.gameObject != null)
                {
                    FindObjectOfType<PlayerController>().Attacked(FindObjectOfType<DeathlingController>().GetDamage());
                }
            }
        } catch (MissingReferenceException e) { }
    }
}
