/*
 * COMP3770 Final Project
 * Enemy.cs
 * Coded by: Michelle Hua
 * Due: December 14, 2020
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 10f;

    public void EnemyDamageTaken(float laserDamage)
    {
        {
            health -= laserDamage;
            if (health <= 0f)
            {
                Die();
            }
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
