using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : MonoBehaviour
{
    AudioManager audioManager;
    private Object thisObj;
    public PlayerHealth playerHealth;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        thisObj = GetComponent<Object>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if ((thisObj.ID).Equals("Coin"))
            {
                PlayerPrefs.SetInt(thisObj.ID, PlayerPrefs.GetInt(thisObj.ID) + 1);
                audioManager.PlaySFX(audioManager.coinClip);
            }
            else if ((thisObj.ID).Equals("Vida"))
            {
                playerHealth.Heal(2);
                audioManager.PlaySFX(audioManager.healthClip);
            }
            Destroy(gameObject);


        }
    }
}
