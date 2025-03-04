using UnityEngine;

public class SeagullBehavior : MonoBehaviour
{
    private AudioSource audioSource;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private bool isHit = false; // Tracks whether the seagull has already been hit
    public GameObject crispPrefab; // Prefab to spawn when destroyed
    public float flashDuration = 0.2f; // Time the seagull stays red
    public float patrolDistance = 2f; // Distance the seagull walks in each direction
    public float patrolSpeed = 2f; // Speed at which the seagull walks

    private Vector3 startingPosition;
    private bool movingRight = true;

    void Start()
    {
        // Get components
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }

        // Set starting position for patrol
        startingPosition = transform.position;
    }

    void Update()
    {
        Patrol();
    }

    public void OnHit()
    {
        // Ensure the seagull can only be hit once
        if (isHit) return;

        isHit = true;

        // Play sound
        if (audioSource != null)
        {
            audioSource.pitch = Random.Range(0.9f, 1.1f);
            audioSource.Play();
        }

        // Flash red
        if (spriteRenderer != null)
        {
            StartCoroutine(FlashRed());
        }

        // Spawn a crisp when destroyed
        DropCrisp();

        // Destroy the seagull after the sound finishes playing
        Destroy(gameObject, audioSource != null ? audioSource.clip.length : 0f);
    }

    private System.Collections.IEnumerator FlashRed()
    {
        // Flash red
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.color = originalColor;
    }

    void Patrol()
    {
        // Move seagull back and forth
        if (movingRight)
        {
            transform.position += Vector3.right * patrolSpeed * Time.deltaTime;

            // Turn around if beyond patrol distance
            if (transform.position.x >= startingPosition.x + patrolDistance)
            {
                FlipDirection();
            }
        }
        else
        {
            transform.position += Vector3.left * patrolSpeed * Time.deltaTime;

            // Turn around if beyond patrol distance
            if (transform.position.x <= startingPosition.x - patrolDistance)
            {
                FlipDirection();
            }
        }
    }

    void FlipDirection()
    {
        movingRight = !movingRight;

        // Reverse the flip logic to match the movement direction
        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = movingRight;
        }
    }

    void DropCrisp()
    {
        if (crispPrefab != null)
        {
            // Instantiate the crisp at the seagull's position
            GameObject crisp = Instantiate(crispPrefab, transform.position, Quaternion.identity);

            // Add a random upward force to the crisp's Rigidbody2D
            Rigidbody2D rb = crisp.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                float randomX = Random.Range(-2f, 2f); // Randomize horizontal force
                float randomY = Random.Range(6f, 12f);  // Randomize upward force
                rb.AddForce(new Vector2(randomX, randomY), ForceMode2D.Impulse);
            }
        }
    }
}
