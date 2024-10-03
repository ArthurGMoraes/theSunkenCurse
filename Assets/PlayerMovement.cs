using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    private float moveDirectionH;
    private float moveDirectionV;

    private Rigidbody2D rb;

    private bool isJumping = false;
    private bool isAboveMaxFuel;

    private float fuel;
    public float maxFuel;

    [SerializeField]
    private Slider fuelSlider;

    private float currentFuel;
    // Called after all objects are initialized
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 3.0f;
        fuel = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();

        Move();

        FuelUpdate();
    }

    void FuelUpdate()
    {
        fuelSlider.value = fuel / maxFuel;

        if (fuel >= maxFuel)
        {
            isAboveMaxFuel = true;
        }
        else
        {
            isAboveMaxFuel = false;
        }

        if (!isJumping && !isAboveMaxFuel)
        {
            fuel += 5;
        }

        if (isJumping)
        {
            fuel -= 5;
        }

    }

    void Move()
    {
        Vector2 newVelocity = Vector2.zero;

        newVelocity.x = moveSpeed * moveDirectionH;

        

        if (isJumping && fuel > 0)
        {
            newVelocity.y = moveSpeed * moveDirectionV;
        } 
        
        

        rb.velocity = newVelocity;
    }

    void GetInput()
    {
        moveDirectionH = Input.GetAxis("Horizontal");
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            isJumping = true;
            moveDirectionV = Input.GetAxis("Vertical");
        }
        else
        {
            isJumping = false;
        }
    }
}
