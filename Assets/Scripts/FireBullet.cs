using UnityEngine;

public class FireBullet : MonoBehaviour
{
    public GameObject bulletPrefab; // The bullet prefab to spawn
    public Transform firePoint; // The point where the bullet is spawned
    public float bulletSpeed = 10f; // Speed of the bullet
    public LayerMask groundLayer; // LayerMask to identify the ground layer

    private bool facingRight = true; // Tracks the player's current facing direction

    void Update()
    {
        // Update the direction based on player input
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            facingRight = false;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            facingRight = true;
        }

        // Fire the bullet when the spacebar is pressed
        if (Input.GetKeyDown(KeyCode.F))
        {
            Fire();
        }
    }

   void Fire()
    {
    // Instantiate the bullet at the firePoint with a 90-degree rotation
    GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0f, 0f, 90f));

    // Adjust rotation of the bullet if facing left
    if (!facingRight)
    {
        // Rotate the bullet by 180 degrees on the Y-axis for proper flipping
        bullet.transform.rotation = Quaternion.Euler(0f, 180f, 90f);
    }

    // Get the Rigidbody2D component of the bullet
    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

    // Apply velocity to the bullet based on the facing direction
    float direction = facingRight ? 1f : -1f;
    rb.velocity = new Vector2(direction * bulletSpeed, 0f);
    }

}