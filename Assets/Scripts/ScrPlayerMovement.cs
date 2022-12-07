using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrPlayerMovement : MonoBehaviour
{
    [SerializeField] float plrSpeed;
    [SerializeField] Rigidbody2D rb;

    Vector2 movement;

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        ;
    }

    // FixedUpdate is frame rate independent

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * plrSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Projectile")) //checks if player is hit by projectile
        {
            Debug.Log("Player Dead!");
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}