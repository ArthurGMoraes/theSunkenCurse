using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chase : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    public float dist;
    public Transform target;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SetTarget();
    }

    void Update()
    {
        Move();
    }

    public void Move()
    {
        if (target == null)
            return;

        // Rotate to look at the player
        Vector3 direction = (target.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;

        // Move towards the player
        if (Vector3.Distance(transform.position, target.position) < dist)
        {
            rb.MovePosition(transform.position + direction * speed * Time.fixedDeltaTime);
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
