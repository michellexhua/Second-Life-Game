/*
 * COMP3770 Final Project
 * GolemDamageByPlayer.cs
 * Coded By: Hannah Stam (103791045)
 * Due: December 14, 2020
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemDamageByPlayer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            FindObjectOfType<GolemController>().Attacked(FindObjectOfType<PlayerController>().GetDamage());

            if (FindObjectOfType<GolemController>().IsDead())
            {
                //If Golem is dead, it destroys the entire golem object, setting the body collider as false
                foreach (Transform child in transform)
                    child.gameObject.SetActive(false);
                Destroy(transform.parent.gameObject, 0.5f);
            }
        }
    }
}