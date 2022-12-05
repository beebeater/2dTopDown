using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrCover : MonoBehaviour
{
    BoxCollider2D col;
    public float health;
    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<BoxCollider2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health == 0)
        {
            //Destroy(gameObject);
            Debug.Log("Crate Gone!");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Projectile")) //checks if crate is hit by projectile
        {
            Debug.Log("Crate Hit!");
            health--;
            Destroy(other.gameObject);
        }
    }
}
