using UnityEngine;

public class Bullet : MonoBehaviour
{
    public LayerMask groundLayer; // LayerMask for the ground
    public float lifeTime = 5f; // Time before the bullet despawns (in seconds)

    void Start()
    {
        // Automatically destroy the bullet after 'lifeTime' seconds
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the bullet collided with the ground
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            Destroy(gameObject); // Destroy the bullet
            return;
        }

        // Check if the bullet collided with a Seagull enemy
        if (collision.gameObject.CompareTag("Seagull"))
        {
            // Notify the seagull of the hit
            SeagullBehavior seagull = collision.gameObject.GetComponent<SeagullBehavior>();
            if (seagull != null)
            {
                seagull.OnHit(); // Delegate all hit logic to the SeagullBehavior script
            }

            // Destroy the bullet
            Destroy(gameObject);
        }
    }
}
