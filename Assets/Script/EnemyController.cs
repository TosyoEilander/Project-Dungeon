using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 2f;
    public int health = 10;
    public Transform player; // Reference to the player

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        MoveTowardsPlayer();
        CheckHealth();
    }

    private void MoveTowardsPlayer()
    {
        if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            Vector3 movement = direction * moveSpeed * Time.deltaTime; // Calculate movement
            rb.MovePosition(rb.position + movement); // Move the enemy
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    private void CheckHealth()
    {
        if (health <= 0)
        {
            Debug.Log("Enemy defeated!");
            Destroy(gameObject); // Remove enemy from scene
        }
    }
}