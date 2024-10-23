using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hit : MonoBehaviour
{
    public int damage = 1;
    public PlayerHealth playerHealth;
    public Transform target;
    public PlayerMovement playerMovement;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.transform == target)
        {
            playerMovement.KbCounter = playerMovement.KbTime;
            if(collision.gameObject.transform.position.x <= transform.position.x)
            {
                playerMovement.KnockFromRight = true;
            }
            if (collision.gameObject.transform.position.x > transform.position.x)
            {
                playerMovement.KnockFromRight = false;
            }
            playerHealth.TakeDamage(damage);
        }
    }

    public void SetTarget()
    {
        if (target == null)
        {

            if (GameObject.FindWithTag("Player") != null)
            {
                target = GameObject.FindWithTag("Player").GetComponent<Transform>();
            }
        }
    }
}
