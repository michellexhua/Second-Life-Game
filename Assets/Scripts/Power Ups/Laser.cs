/*
 * COMP3770 Final Project
 * Laser.cs
 * Coded by: Michelle Hua
 * Due: December 14, 2020
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float damage = 10f;

    private LineRenderer lr;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            lr.enabled = true;
            var Ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            lr.SetPosition(0, transform.position);
            RaycastHit hit;

            if (Physics.Raycast(Ray, out hit))
            {
                lr.SetPosition(1, hit.point * 100);

                Enemy enemy = hit.transform.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.EnemyDamageTaken(damage);
                }
            }
        }
        else
        {
            lr.enabled = false;
        }
    }
}