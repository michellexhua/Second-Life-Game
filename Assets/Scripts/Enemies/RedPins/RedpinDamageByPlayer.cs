using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedpinDamageByPlayer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //If it collides with player, it is attacked.
        if (other.name == "Player")
        {
            FindObjectOfType<RedpinController>().Attacked(FindObjectOfType<PlayerController>().GetDamage());

            //If redpin is dead, it destroys the entire redpin object, setting the body collider as false
            if (FindObjectOfType<RedpinController>().IsDead())
            {
                foreach (Transform child in transform)
                    child.gameObject.SetActive(false);
                Destroy(transform.parent.gameObject, 0.5f);
            }
        }
    }
}
