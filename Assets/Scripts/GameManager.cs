using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TarodevController; // Ensure PlayerController namespace is recognized
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI crispCounterText; // UI text for crisps
    public GameObject player; // Player GameObject
    public Transform playerStartPosition; // Player's starting position
    public CanvasGroup retryPanelCanvasGroup; // Retry panel for fading
    public Button retryButton; // Retry button
    public float fadeDuration = 1f; // Duration for fading
    private int crispsCollected = 0; // Current crisps collected
    public int totalCrispsNeeded = 18; // Total crisps to collect

    private PlayerController playerController; // Reference to PlayerController script
    private Vector3[] seagullStartPositions; // To store initial positions of seagulls
    private GameObject[] seagulls; // Array to hold all seagulls in the scene

    void Start()
    {
        playerController = player.GetComponent<PlayerController>(); // Get PlayerController script
        retryPanelCanvasGroup.alpha = 0; // Initially hide retry panel
        retryPanelCanvasGroup.interactable = false;
        retryPanelCanvasGroup.blocksRaycasts = false;

        retryButton.onClick.AddListener(Retry); // Hook up Retry button
        UpdateCrispCounter();

        // Find all seagulls in the scene and save their start positions
        seagulls = GameObject.FindGameObjectsWithTag("Seagull");
        seagullStartPositions = new Vector3[seagulls.Length];
        for (int i = 0; i < seagulls.Length; i++)
        {
            seagullStartPositions[i] = seagulls[i].transform.position;
        }
    }

    public void CheckWinCondition()
    {
        if (crispsCollected >= totalCrispsNeeded)
        {
            Debug.Log("You win!");
            WinGame();
        }
        else
        {
            Debug.Log("Not enough crisps to win!");
        }
    }

    public void AddCrisp()
    {
        crispsCollected++;
        UpdateCrispCounter();

        if (crispsCollected >= totalCrispsNeeded)
        {
            Debug.Log("All crisps collected! You win!");
        }
    }

    private void WinGame()
    {
        // Logic when the player wins
        Debug.Log("Victory! Loading the win screen...");

        // Example: Load a "Win" scene (make sure to create this scene)
        SceneManager.LoadScene("WinScreen");
    }

    private void UpdateCrispCounter()
    {
        crispCounterText.text = $"{crispsCollected}/{totalCrispsNeeded}";
    }

    public void ResetLevel()
    {
        // Disable player's movement
        if (playerController != null)
        {
            playerController.canMove = false;
        }

        // Hide the player
        player.SetActive(false);

        // Disable all seagulls
        foreach (GameObject seagull in seagulls)
        {
            if (seagull != null)
            {
                seagull.SetActive(false);
            }
        }

        // Fade to black and show retry panel
        StartCoroutine(FadeAndReset());
    }

    private System.Collections.IEnumerator FadeAndReset()
    {
        // Fade retry panel to black
        yield return StartCoroutine(FadeCanvasGroup(retryPanelCanvasGroup, 0, 1));

        // Reset crisps count
        crispsCollected = 0;
        UpdateCrispCounter();
    }

    public void Retry()
    {
    Debug.Log("Retrying level...");

    // Enable and reset the player
    player.SetActive(true);
    if (playerStartPosition != null)
    {
        player.transform.position = playerStartPosition.position;
    }

    // Enable player's movement
    if (playerController != null)
    {
        playerController.canMove = true;
    }

    // Reset and re-enable all seagulls
    for (int i = 0; i < seagulls.Length; i++)
    {
        if (seagulls[i] != null)
        {
            // Reset seagull position to their initial starting positions
            seagulls[i].transform.position = seagullStartPositions[i];

            // Re-enable the seagull GameObject
            seagulls[i].SetActive(true);

            // Restore their original layer
            seagulls[i].layer = LayerMask.NameToLayer("Seagull");

            // Re-enable the collider for the seagull
            Collider2D collider = seagulls[i].GetComponent<Collider2D>();
            if (collider != null)
            {
                collider.enabled = true; // Re-enable the collider
            }

            // Reset seagull behavior if there is a custom script
            SeagullBehavior seagullBehavior = seagulls[i].GetComponent<SeagullBehavior>();
            if (seagullBehavior != null)
            {
                seagullBehavior.ResetSeagull(); // Call the reset method to restore their default state
            }
        }
    }

    // Optionally reset the crisp counter (if retry should clear progress)
    crispsCollected = 0;
    UpdateCrispCounter();

    // Fade out the retry panel
    StartCoroutine(FadeCanvasGroup(retryPanelCanvasGroup, 1, 0));
    }


    private System.Collections.IEnumerator FadeCanvasGroup(CanvasGroup canvasGroup, float startAlpha, float endAlpha)
    {
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, timer / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = endAlpha;

        // Enable or disable interaction based on fade direction
        canvasGroup.interactable = endAlpha > 0;
        canvasGroup.blocksRaycasts = endAlpha > 0;
    }
}
