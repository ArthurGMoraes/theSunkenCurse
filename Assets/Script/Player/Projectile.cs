using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float lifetime;
    public float distance;
    public LayerMask solid;

    public int damage = 1;
    // Start is called before the first frame update
    void Start()
    {
        damage = 1;
        Invoke("DestroyProjectile", lifetime);

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, solid);
        if(hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Enemy"))
            {
                enemyHealth health = hitInfo.collider.GetComponent<enemyHealth>();
                if (health != null)
                {
                    health.TakeDamage(damage);
                    Debug.Log("receba");
                }
                Debug.Log("tiro");
            }
            if (!(hitInfo.collider.CompareTag("Player"))){
                Debug.Log("destruiu");
                DestroyProjectile();
            }
        }
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }


    public void DestroyProjectile()
    {
        //Instatiante(destroyEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
