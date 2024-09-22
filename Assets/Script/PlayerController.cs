using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 100f;
    public int health = 10;

    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Move();
        Jump();
        CheckHealth();
    }

    private void Move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal"); // A/D or Left/Right
        float moveVertical = Input.GetAxis("Vertical"); // W/S or Up/Down
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical) * moveSpeed;
        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);
    }

    private void Jump()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.J)) // J for jump
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && rb.velocity.y < 0)
        {
            collision.gameObject.GetComponent<EnemyController>().TakeDamage(1);
            isGrounded = true; // Allow jumping on the enemy
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; // Set grounded status
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Enemy"))
        {
            isGrounded = false; // Set grounded status
        }
    }

    private void CheckHealth()
    {
        if (health <= 0)
        {
            Debug.Log("Player defeated!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Restart scene
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}