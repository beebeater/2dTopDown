using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScrPlayerMovement : MonoBehaviour
{
    [SerializeField] float walkSpeed;


    [SerializeField] Rigidbody2D rb;
    Animator anim;

    //float horizontalInput;
    //float verticalInput;
    Vector2 moveInput;
    bool isDead;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        //horizontalInput = Input.GetAxisRaw("Horizontal");
        //verticalInput = Input.GetAxisRaw("Vertical");
        if (!isDead)
        {
            moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
            anim.SetFloat("moveX", moveInput.x);
            anim.SetFloat("moveY", moveInput.y);
        }
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
            isDead = true;
            StartCoroutine(UIWaitTime());
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }

    }
    IEnumerator UIWaitTime()
    {
        yield return new WaitForSeconds(2);

        SceneManager.LoadScene(2);
    }
}