using UnityEngine;

public class WinningZone : MonoBehaviour
{
    public GameManager gameManager; // Reference to the GameManager

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player enters the WinningZone
        if (collision.CompareTag("Player"))
        {
            if (gameManager != null)
            {
                gameManager.CheckWinCondition(); // Notify GameManager to check win logic
            }
        }
    }
}
