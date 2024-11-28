using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : MonoBehaviour
{
    private Object thisObj;
    public PlayerHealth playerHealth;

    private void Awake()
    {
        thisObj = GetComponent<Object>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if ((thisObj.ID).Equals("Coin"))
            {
                PlayerPrefs.SetInt(thisObj.ID, PlayerPrefs.GetInt(thisObj.ID) + 1); 
            }
            else if ((thisObj.ID).Equals("Vida"))
            {
                playerHealth.Heal(2);
            }
            Destroy(gameObject);


        }
    }
}
