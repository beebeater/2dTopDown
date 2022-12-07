using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrPlayerMovement : MonoBehaviour
{
    [SerializeField] float walkSpeed;
    [SerializeField] float maxSpeed;

    [SerializeField] Rigidbody2D rb;

    float horizontalInput;
    float verticalInput;

    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    // FixedUpdate is frame rate independent

    void FixedUpdate()
    {
        if (horizontalInput != 0 || verticalInput !=0)
        {
            if ( horizontalInput != 0 && verticalInput != 0)
            {
                horizontalInput = horizontalInput * maxSpeed;
                verticalInput = verticalInput * maxSpeed;
            }
            rb.velocity = new Vector2(horizontalInput * walkSpeed, verticalInput * walkSpeed);
        }
        else
        {
            rb.velocity = new Vector2(0f, 0f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile")) //checks if player is hit by projectile
        {
            Debug.Log("Player Dead!");
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}