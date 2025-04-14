using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Bullet : MonoBehaviour
{
    public float speed = 5f;
    public float lifetime = 3f;
    public Vector2 direction = Vector2.right;

    void Start()
    {
        Destroy(gameObject, lifetime); // Destroy bullet after a certain time
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy_ghost")) // Check if the object has the "Enemy" tag
        {
            GhostControl ghost = collision.GetComponent<GhostControl>();
            if (ghost != null)
            {
                ghost.TriggerDeath(); // Call the method to play the death animation
            }
            Destroy(gameObject); // Destroy the bullet
        }
        else if (collision.CompareTag("Enemy_scarecrow")) // Check if the object has the "Enemy" tag
        {
            ScarecrowController scarecrow = collision.GetComponent<ScarecrowController>();
            if (scarecrow != null)
            {
                scarecrow.DestroyScarecrow(); // Call the method to play the death animation
            }
            Destroy(gameObject); // Destroy the bullet
        }
        else if(!collision.CompareTag("Player")) 
        {
            if (collision.CompareTag("Ground"))
            {
                Destroy(gameObject); // Destroy the bullet if it hits the ground
            }
        }
    }

}
