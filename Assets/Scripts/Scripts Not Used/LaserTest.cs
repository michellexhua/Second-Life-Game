/*
 * COMP3770 Final Project
 * LaserTest.cs
 * Coded by: Michelle Hua
 * Due: December 14, 2020
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTest : MonoBehaviour
{
    public float rayLength = 200f;

    [SerializeField]
    private LayerMask layerMask;

    // Update is called once per frame
    void Update()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;
        Vector3 mouseWorldSpace = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        //draws ray
        Debug.DrawRay(transform.position, mouseWorldSpace, Color.red);
        Ray ray = new Ray(transform.position, mouseWorldSpace);

        //hits all enemies in path
        var multipleHits = Physics.RaycastAll(ray, rayLength, layerMask);
        foreach (var raycastHit in multipleHits)
        {
            raycastHit.collider.GetComponent<Renderer>().material.color = Color.red;
        }
    }
}
