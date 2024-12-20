using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Drawing;

public class enemyHealth : MonoBehaviour
{
    // private bool isDead;
    public int maxHealth = 3;
    public int health;

    public GameObject coin;
    public GameManager gameManager;
    public string ID = "Enemy";


    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(int dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            health = 0;
            if (ID.Equals("Boss")){
                gameManager.gameWin();
            }
            
            Instantiate(coin, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
    }
}
