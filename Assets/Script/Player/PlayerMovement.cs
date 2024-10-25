using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 2;
    public float jumpForce = 10;
    private float moveDirectionH;
    private float moveDirectionV;
    private bool facingRight = true;
    private bool facingUp = true;
    public Animator animator;

    private Rigidbody2D rb;

    private bool isJumping = false;
    private bool isAboveMaxFuel = false;

    public float fuel = 1000;
    public float maxFuel = 2000;
    public float addFuel = 4;
    public float useFuel = 7;

    [SerializeField]
    private Slider fuelSlider;

    public float KbForce;
    public float KbCounter;      //tempo restante
    public float KbTime;         // duracao     
    public bool KnockFromRight;  // direcao

    public float upgradeValue = 1.00f;

    // time 0.5 kb 2.3 parece debaixo d�gue
    // time 0.2 kb 5 fica bom mas acho que nao combina com o tema

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

    void rotateCharacter()
    {
        facingUp = !facingUp;
        transform.Rotate(180f, 0f, 0f);
    }

    private void ResetRotation()
    {
        facingUp = true;
        transform.rotation = Quaternion.Euler(180, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
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

        if (moveDirectionV != 0)
        {
            if (facingUp && moveDirectionV < 0)
            {
                rotateCharacter();
            }
            else if (!facingUp && moveDirectionV > 0)
            {
                rotateCharacter();
            }
        }
        else if (facingUp == false)
        {
            ResetRotation();
        }
           
        
        
        

        animator.SetBool("isJumping", isJumping);
    }

    void FuelUpdate()
    {
        fuelSlider.value = fuel / maxFuel;

        if (fuel >= maxFuel)
        {
            isAboveMaxFuel = true;
            fuel = maxFuel;
        }
        else
        {
            isAboveMaxFuel = false;
        }

        if (!isJumping && !isAboveMaxFuel)
        {
            fuel += addFuel;
        }

        if (isJumping)
        {
            fuel -= useFuel;
            if (fuel <= 0)
            {
                fuel = 0;
            }
        }

    }

    void Move()
    {
        if (KbCounter <= 0)
        {
            Vector2 newVelocity = Vector2.zero;

            newVelocity.x = moveSpeed * moveDirectionH;



            if (isJumping && fuel > 0)
            {
                newVelocity.y = (moveSpeed * moveDirectionV) * upgradeValue;
            }

            rb.velocity = newVelocity;
            KbCounter = 0;
        }
        else
        {
            if (KnockFromRight == true)
            {
                rb.velocity = new Vector2(-KbForce, KbForce);
            }
            if (KnockFromRight == false)
            {
                rb.velocity = new Vector2(KbForce, KbForce);
            }
            KbCounter -= Time.deltaTime;
        }
    }

    void GetInput()
    {
        moveDirectionH = Input.GetAxis("Horizontal");
        moveDirectionV = Input.GetAxis("Vertical");
        if (moveDirectionV != 0)
        {
            isJumping = true;
        }
        else
        {
            isJumping = false;
        }
    }
}
