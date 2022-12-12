using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrPlayerMovement : MonoBehaviour
{
    [SerializeField] float walkSpeed;


    [SerializeField] Rigidbody2D rb;

    //float horizontalInput;
    //float verticalInput;
    Vector2 moveInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        //horizontalInput = Input.GetAxisRaw("Horizontal");
        //verticalInput = Input.GetAxisRaw("Vertical");
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    }

    // FixedUpdate is frame rate independent

    void FixedUpdate()
    {
        if (moveInput.x != 0 || moveInput.y !=0)
        {
            /*if ( horizontalInput != 0 && verticalInput != 0)
            *{
             *   horizontalInput = horizontalInput * maxSpeed;
              *  verticalInput = verticalInput * maxSpeed;
            }*/
            rb.velocity = new Vector2(moveInput.x * walkSpeed , moveInput.y * walkSpeed);
            Debug.Log("speed"+rb.velocity);
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