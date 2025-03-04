using UnityEngine;

public class ShootingScipt : MonoBehaviour
{
    public GameObject bulletPrefab; // Reference to the bullet prefab
    public Transform bulletSpawnPoint; // Position from which bullets are spawned
    public float bulletSpeed = 10f; // Speed of the bullet

    private Vector2 facingDirection = Vector2.right; // Default direction the player is looking (right)

    void Update()
    {
        // Update the facing direction based on player input or movement
        UpdateFacingDirection();

        // Shoot when the player presses the Fire button (e.g., Spacebar)
        if (Input.GetKeyDown(KeyCode.F))
        {
            Shoot();
        }
    }

    private void UpdateFacingDirection()
    {
        // Example: Set facing direction based on horizontal movement (change this as per your movement logic)
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        if (horizontalInput > 0)
            facingDirection = Vector2.right; // Looking right
        else if (horizontalInput < 0)
            facingDirection = Vector2.left; // Looking left
    }

    private void Shoot()
    {
        // Instantiate the bullet at the spawn point
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);

        // Get the Rigidbody2D of the bullet to apply velocity
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        if (bulletRb != null)
        {
            // Apply velocity to the bullet in the direction the player is facing
            bulletRb.linearVelocity = facingDirection * bulletSpeed;
        }
    }

   
    private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Ground"))
            {
                Destroy(collision.gameObject);
            }
        }
    
}
