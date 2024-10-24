using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{
    private bool isDead;

    public GameManager gameManager;
    public int maxHealth = 10;
    public int health;

    public static event Action OnPlayerDamaged;
    public static event Action OnPlayerDeath;

    // Start is called before the first frame update
    void Start()
    {
       health = maxHealth;
    }
    public void TakeDamage(int dmg){
        health -= dmg;
        OnPlayerDamaged?.Invoke();
        if (health <= 0)
        {
            isDead = true;
            health = 0;
            Debug.Log("You're dead");
            Time.timeScale = 0;
            gameManager.gameOver();
            OnPlayerDeath?.Invoke();
        }
    }
}
