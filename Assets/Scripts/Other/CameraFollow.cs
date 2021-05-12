/*
 * COMP3770 Final Project
 * CameraFollow.cs
 * Coded By: Hannah Stam (103791045)
 * Due: December 14, 2020
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;


    // Start is called before the first frame update
    void Start()
    {
        offset = new Vector3(0, 2.5f, -10);
    }

    // Update is called once per frame
    void Update()
    {
        //Camera will never move to the left
        if(player.transform.position.x >= transform.position.x)
        {
            transform.position = player.transform.position + offset;
        }        
    }

}
