using UnityEngine;

public class CrispBehavior : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Assuming your player is tagged as "Player"
        {
            // Notify the GameManager that a crisp was collected
            GameManager gameManager = FindObjectOfType<GameManager>();
            if (gameManager != null)
            {
                gameManager.AddCrisp();
            }

            // Destroy the crisp
            Destroy(gameObject);
        }
    }
}
