using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hit : MonoBehaviour
{
    public int damage = 1;
    public PlayerHealth playerHealth;
    public Transform target;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.transform == target)
        {
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
