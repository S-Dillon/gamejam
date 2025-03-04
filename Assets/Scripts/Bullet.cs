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
    }

    // Check if the bullet collided with a Seagull enemy
    if (collision.gameObject.CompareTag("Seagull"))
    {
        // Notify the seagull of the hit
        SeagullBehavior seagull = collision.gameObject.GetComponent<SeagullBehavior>();
        if (seagull != null)
        {
            seagull.OnHit();
        }

        // Destroy the bullet
        Destroy(gameObject);
    }
    }

    void PlaySeagullSound(GameObject seagull)
    {
    // Get the AudioSource component on the seagull
    AudioSource audioSource = seagull.GetComponent<AudioSource>();

    if (audioSource != null)
    {
        // Randomize the pitch slightly for variation
        audioSource.pitch = Random.Range(0.9f, 1.1f);

        // Play the audio clip
        audioSource.Play();

        // Destroy the seagull after the sound finishes playing
        Destroy(seagull, audioSource.clip.length);
    }
    }

}
