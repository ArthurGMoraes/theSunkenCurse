using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth;
    public int health;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }
    public void TakeDamage(int dmg){
        health -= dmg;
        if (health <= 0)
        {   
            Destroy(gameObject);   
        }
    }
}
