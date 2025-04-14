using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostControl : MonoBehaviour
{
    public float shootingInterval = 2f;
    public GameObject bulletPrefab; 
    public Transform firePoint; 
    public GameObject player;
    public Animator animator;
    private Transform playerTransform;
    
    private float shootingTimer;

    void Start()
    {

        
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("Player not found! Make sure the player has the 'Player' tag.");
        }

        animator = GetComponent<Animator>();
        shootingTimer = shootingInterval;
    }

    void Update()
    {
        shootingTimer -= Time.deltaTime;

        if (shootingTimer <= 0)
        {
            Shoot();
            shootingTimer = shootingInterval; // Reset timer
        }
    }

    public void TriggerDeath()
    {
        animator.SetTrigger("Die"); // Play the "Die" animation
        StartCoroutine(RemoveAfterDeath());
    }

    private IEnumerator RemoveAfterDeath()
    {
        // Wait for the animation to finish
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject); // Remove the enemy from the game
    }

    void Shoot()
    {
        // Play shooting animation
        animator.SetTrigger("Shoot");

        // Instantiate the bullet
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        
        Enemy_Bullet bulletScript = bullet.GetComponent<Enemy_Bullet>();
        Vector2 targetPosition = new Vector2(playerTransform.position.x, playerTransform.position.y); 
        bulletScript.direction = (targetPosition - (Vector2)firePoint.position).normalized;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Punch_hitbox"))
        {
            Destroy(gameObject); // Destroy the enemy
        }
    }
}