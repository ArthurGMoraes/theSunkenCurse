using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    private float moveDirectionH;
    private float moveDirectionV;
    private bool facingRight = true;
    public Animator animator;

    private Rigidbody2D rb;

    private bool isJumping = false;
    private bool isAboveMaxFuel = false;

    public float fuel;
    public float maxFuel;

    [SerializeField]
    private Slider fuelSlider;

    private float currentFuel;
    // Called after all objects are initialized
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 3.0f;
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();

        Animate();
    }

    private void FixedUpdate()
    {
        Move();

        FuelUpdate();
    }

    void FlipCharacter()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    void Animate()
    {
        if (facingRight && moveDirectionH < 0)
        {
            FlipCharacter();
        }
        else if (!facingRight && moveDirectionH > 0)
        {
            FlipCharacter();
        }

        animator.SetBool("isJumping", isJumping);
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
