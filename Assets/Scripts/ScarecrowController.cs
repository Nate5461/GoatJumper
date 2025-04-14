using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScarecrowController : MonoBehaviour
{
     public float speed = 2f;
    public float moveDistance = 2.5f;
    private bool movingLeft = true;
    private Vector2 startingPosition;

    private void Start()
    {
        startingPosition = transform.position;
    }

    private void Update()
    {
        // Move the scarecrow left and right
        if (movingLeft)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            if (Vector2.Distance(startingPosition, transform.position) >= moveDistance)
            {
                movingLeft = false;
            }
        }
        else
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            if (Vector2.Distance(startingPosition, transform.position) >= moveDistance)
            {
                movingLeft = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Punch_hitbox"))
        {
            Destroy(gameObject); // Destroy the scarecrow
        }
        else if (collision.CompareTag("Player"))
        {
            Destroy(gameObject); // Destroy the scarecrow
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload the current scene
        }
    }

    public void DestroyScarecrow()
    {
        // Play death animation (if applicable) and destroy the scarecrow
        Destroy(gameObject);
    }

}
