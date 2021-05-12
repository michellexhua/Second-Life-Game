/*
COMP 3770: Final Project
PaladinDamageToPlayer.cs
Coded by: Hannah Stam (103791045)
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaladinDamageToPlayer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //If it collides into player, it attacks player
        if (other.gameObject.tag == "Player")
        {
            FindObjectOfType<PlayerController>().Attacked(FindObjectOfType<PaladinController>().GetDamage());
        }
    }
}
