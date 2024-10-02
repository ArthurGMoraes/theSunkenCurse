using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;

    private Rigidbody2D rb;

<<<<<<< HEAD
<<<<<<< Updated upstream
=======
=======
>>>>>>> 2de719d05c7ccbac0672db58f46d09e40bebe6fd
    private bool isJumping;
    private bool isAboveMaxFuel;

    private float fuel;
<<<<<<< HEAD
    public float maxFuel;
=======
    private float maxFuel;
>>>>>>> 2de719d05c7ccbac0672db58f46d09e40bebe6fd

    [SerializeField]
    private Slider fuelSlider;

    private float currentFuel;
<<<<<<< HEAD
>>>>>>> Stashed changes
=======
>>>>>>> 2de719d05c7ccbac0672db58f46d09e40bebe6fd
    // Called after all objects are initialized
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
<<<<<<< HEAD
<<<<<<< Updated upstream
=======
        rb.gravityScale = 3.0f;
        fuel = 100f;
>>>>>>> Stashed changes
=======
        rb.gravityScale = 3.0f;
        fuel = 100f;
        maxFuel = 10000f;
>>>>>>> 2de719d05c7ccbac0672db58f46d09e40bebe6fd
    }

    // Update is called once per frame
    void Update()
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

        Vector2 newVelocity = Vector2.zero;

        if (Input.GetKey(KeyCode.D))
        {
            newVelocity.x = moveSpeed;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            newVelocity.x = -moveSpeed;
        }


        if (Input.GetKey(KeyCode.W))
        {
            if (fuel > 0)
            {
                isJumping = true;
                newVelocity.y = moveSpeed;
                fuel -= 5;
            }
        }
        else if (Input.GetKey(KeyCode.S))
        {
            if (fuel > 0)
            {
                isJumping = true;
                newVelocity.y = -moveSpeed;
                fuel -= 5;
            }
        }
        else
        {
            isJumping = false;
        }

        Debug.Log("Fuel: " + fuel + " Jumping: " + isJumping);

        if (!isJumping && !isAboveMaxFuel)
        {
            fuel += 5;
        }


        rb.velocity = newVelocity;
    }
}
