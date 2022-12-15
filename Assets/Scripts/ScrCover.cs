using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrCover : MonoBehaviour
{
    BoxCollider2D col;
    public float health;
    SpriteRenderer spriteRenderer;

    [Header("Sprite Settings:")]
    public Sprite[] destLevel;

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health == 0)
        {
            Destroy(gameObject);
            Debug.Log("Crate Gone!");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Projectile")) //checks if crate is hit by projectile
        {
            Debug.Log("Crate Hit!");
            health--;
            spriteRenderer.sprite = destLevel[(int)health - 1];
            //Destroy(collision.gameObject);
            collision.gameObject.SetActive(false);
        }
    }
}
