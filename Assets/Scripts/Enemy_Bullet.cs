using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy_Bullet : MonoBehaviour
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
        Debug.Log("Bullet hit: " + collision.name);
        // Destroy the bullet if it hits anything except the enemy itself
        if(collision.CompareTag("Player"))
        {
            Debug.Log("Player hit by bullet!");
            Destroy(gameObject);

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

}
