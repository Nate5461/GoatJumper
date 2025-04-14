using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    public float jumpForce = 5f;
    public Rigidbody2D rb;
    public Animator animator;

    private bool isGrounded = true;
    private float horizontalSpeed; // Track horizontal speed when jumping

    public GameObject bulletPrefab; // Bullet prefab to instantiate
    public Transform firePoint;
    public GameObject punchHitbox;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float move = Input.GetAxis("Horizontal");

        if (isGrounded)
        {
            // Set speed based on walk or run
            bool isRunning = Input.GetKey(KeyCode.LeftShift);
            horizontalSpeed = isRunning ? runSpeed : walkSpeed;
            rb.velocity = new Vector2(move * horizontalSpeed, rb.velocity.y);

            // Set animator parameters
            if (move != 0)
            {
                animator.SetBool("isWalking", !isRunning);
                animator.SetBool("isRunning", isRunning);
                transform.localScale = new Vector3(Mathf.Sign(move), 1, 1);
            }
            else
            {
                animator.SetBool("isWalking", false);
                animator.SetBool("isRunning", false);
            }
        }
        else
        {
            // Apply saved horizontal speed when in the air
            rb.velocity = new Vector2(move * horizontalSpeed, rb.velocity.y);
        }

        // Jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            animator.SetTrigger("jumpTrigger");
            isGrounded = false;
        }

        // Shooting
        if (Input.GetKeyDown(KeyCode.E))
        {
            Shoot();
        } else if (Input.GetKeyDown(KeyCode.Q))
        {
            Punch();
        }

    }

    void Shoot(){
        animator.SetTrigger("shootTrigger");
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Player_Bullet bulletScript = bullet.GetComponent<Player_Bullet>();
        bulletScript.direction = transform.localScale.x > 0 ? Vector2.right : Vector2.left;

    }

    void Punch()
    {
        animator.SetTrigger("punchTrigger");
        // Activate the punch hitbox
        punchHitbox.SetActive(true);
        StartCoroutine(DisablePunchHitboxAfterDelay(0.5f)); // Adjust the duration as needed
    }

    private IEnumerator DisablePunchHitboxAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        punchHitbox.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player has landed on the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.SetBool("isGrounded", true); // Update Animator parameter
            animator.ResetTrigger("jumpTrigger");
        } else if (collision.gameObject.CompareTag("Finish"))
        {
            Debug.Log("Game Over");
            GameController.instance.EndGame();
        }
    }
}
