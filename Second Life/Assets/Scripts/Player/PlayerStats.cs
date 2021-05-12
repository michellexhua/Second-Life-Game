/*
 * COMP3770 Final Project
 * PlayerStats.cs
 * Coded by: Michelle Hua
 * Due: December 14, 2020
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private bool hasImmunity;
    private bool shieldPowerup;
    private bool doubleDamPowerup;
    private bool laserPowerup;
    private bool teleportPowerup;
    private bool instantKillPowerup;
    Vector3 teleportPosition;

    private void OnTriggerEnter(Collider other)
    {
        //Collects Immunity Powerup (last for 5 seconds)
        if (other.CompareTag("Immune"))
        {
            gameObject.GetComponent<Renderer>().material.color = Color.blue;
            Debug.Log("Immunity activated for 5 seconds");
            hasImmunity = true;
            shieldPowerup = false;
            doubleDamPowerup = false;
            laserPowerup = false;
            teleportPowerup = false;
            instantKillPowerup = false;
            StartCoroutine(Countdown(other));
        }

        //Collects Shield Powerup (can take 1 hit)
        if (other.CompareTag("Shield"))
        {
            gameObject.GetComponent<Renderer>().material.color = Color.yellow;
            Debug.Log("Shield activated");
            hasImmunity = true;
            doubleDamPowerup = false;
            laserPowerup = false;
            teleportPowerup = false;
            instantKillPowerup = false;
            shieldPowerup = hasImmunity;
        }
        if (shieldPowerup == true && other.CompareTag("Enemy"))
        {
            hasImmunity = false;
            shieldPowerup = false;
            Debug.Log("Power up false");
        }

        //Collects Double Damage Powerup
        if (other.CompareTag("DoubleDam"))
        {
            gameObject.GetComponent<Renderer>().material.color = Color.red;
            Debug.Log("Double damage activated for 10 seconds");
            doubleDamPowerup = true;
            hasImmunity = false;
            shieldPowerup = false;
            laserPowerup = false;
            teleportPowerup = false;
            instantKillPowerup = false;
            int damage = FindObjectOfType<PlayerController>().GetDamage();
            FindObjectOfType<PlayerController>().SetDamage(damage *= 2);
            StartCoroutine(DoubleDamCountdown(other));
        }


        //Collects Teleport Powerup (player immune for 2 seconds after teleporting)
        if (other.CompareTag("Teleport"))
        {
            int TPPowerup = FindObjectOfType<PlayerController>().GetTPPowerupCollected();
            if (TPPowerup < 4)
            {
                FindObjectOfType<PlayerController>().SetTPPowerupCollected(TPPowerup += 1);
            }
            else
            {
                gameObject.GetComponent<Renderer>().material.color = Color.grey;
                FindObjectOfType<PlayerController>().SetTPPowerupCollected(TPPowerup += 1);
                teleportPowerup = true;
                hasImmunity = false;
                shieldPowerup = false;
                doubleDamPowerup = false;
                laserPowerup = false;
                instantKillPowerup = false;
            }
        }

        //Collects Laser Powerup (last for 30 seconds)
        if (other.CompareTag("Laser"))
        {
            gameObject.GetComponent<Renderer>().material.color = Color.white;
            Debug.Log("Laser activated for 30 seconds");
            laserPowerup = true;
            hasImmunity = false;
            shieldPowerup = false;
            doubleDamPowerup = false;
            teleportPowerup = false;
            instantKillPowerup = false;
            transform.GetChild(0).gameObject.SetActive(true);
            StartCoroutine(LaserCountdown(other));
        }

        //Collects Instant Kill Powerup (last for 5 seconds)
        if (other.CompareTag("InstantKill"))
        {
            gameObject.GetComponent<Renderer>().material.color = Color.green;
            Debug.Log("Instant kill activated for 5 seconds");
            instantKillPowerup = true;
            hasImmunity = false;
            shieldPowerup = false;
            doubleDamPowerup = false;
            laserPowerup = false;
            teleportPowerup = false;
            StartCoroutine(InstantKillCountdown(other));
        }
        if (instantKillPowerup == true && other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
        }
    }

    //Count down for immunity powerup
    IEnumerator Countdown(Collider player)
    {
        yield return new WaitForSeconds(5);
        Debug.Log("Immunity Expired");
        hasImmunity = false;
    }

    //Count down for teleport immunity powerup
    IEnumerator TeleportCountdown()
    {
        FindObjectOfType<PlayerController>().SetTPPowerupCollected(0);
        Debug.Log("Immunity activated for 2 seconds");
        yield return new WaitForSeconds(2);
        Debug.Log("Immunity Expired");
        hasImmunity = false;
    }

    //Count down for double damage powerup
    IEnumerator DoubleDamCountdown(Collider player)
    {
        yield return new WaitForSeconds(10);
        Debug.Log("Double Damage Expired");
        doubleDamPowerup = false;
        int damage = FindObjectOfType<PlayerController>().GetDamage();
        FindObjectOfType<PlayerController>().SetDamage(damage /= 2);
    }

    //Count down for laser powerup
    IEnumerator LaserCountdown(Collider player)
    {
        yield return new WaitForSeconds(30);
        Debug.Log("Laser Expired");
        laserPowerup = false;
        transform.GetChild(0).gameObject.SetActive(false);
    }

    //Count down for instant kill power up
    IEnumerator InstantKillCountdown(Collider player)
    {
        yield return new WaitForSeconds(5);
        Debug.Log("Instant Kill Expired");
        instantKillPowerup = false;
    }

    //Takes damage of whatever amount is passed to the function
    public void TakeDamage(int amount)
    {
        if (hasImmunity == false)
        {
            FindObjectOfType<PlayerController>().Attacked(amount);
            if (FindObjectOfType<PlayerController>().GetCurrentHealth() <= 0f)
            {
                Die();
            }
        }
    }

    //Player dies
    void Die()
    {
        Debug.Log("Player is dead");
    }

    void Start()
    {
        //Obtains players position
        teleportPosition = transform.position;
        hasImmunity = false;
        shieldPowerup = false;
        doubleDamPowerup = false;
        laserPowerup = false;
        teleportPowerup = false;
        instantKillPowerup = false;
    }

    void Update()
    {
        //Teleports player to clicked position
        if (Input.GetMouseButtonDown(0) && teleportPowerup == true)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                teleportPosition = hit.point;
                transform.position = teleportPosition;
                teleportPowerup = false;
                hasImmunity = true;
                StartCoroutine(TeleportCountdown());
            }
        }
        if ((hasImmunity || shieldPowerup || doubleDamPowerup || laserPowerup || teleportPowerup || instantKillPowerup) == false)
        {
            gameObject.GetComponent<Renderer>().material.color = new Color32(0, 203, 255, 255);
        }
    }

}
