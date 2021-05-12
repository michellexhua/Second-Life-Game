/*
COMP 3770: Final Project
PaladinDamageByPlayer.cs
Coded by: Hannah Stam (103791045)
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaladinDamageByPlayer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //If it collides with player, it is attacked.
        if (other.name == "Player")
        {
            FindObjectOfType<PaladinController>().Attacked(FindObjectOfType<PlayerController>().GetDamage());

            //If redpin is dead, it destroys the entire redpin object, setting the body collider as false
            if (FindObjectOfType<PaladinController>().IsDead())
            {
                foreach (Transform child in transform)
                    child.gameObject.SetActive(false);
                Destroy(transform.parent.gameObject, 0.5f);
            }
        }
    }
}
