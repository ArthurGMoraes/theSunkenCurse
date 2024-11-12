using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour
{
    public GameObject attackPoint;
    public float raio;
    public LayerMask enemies;

    public float attackRate;
    float attackTime = 0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= attackTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                attack(1);
                attackTime = Time.time + 1f /attackRate;
                //Debug.Log("re");
            }
        }
        
    }

    public void attack(int dmg)
    {
        Collider2D[] enemy = Physics2D.OverlapCircleAll(attackPoint.transform.position, raio, enemies);
        foreach (Collider2D e in enemy)
        {
            enemyHealth health = e.GetComponent<enemyHealth>();
            if (health != null)
            {
                health.TakeDamage(dmg);
                Debug.Log("receba");
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.transform.position, raio);
    }
}
