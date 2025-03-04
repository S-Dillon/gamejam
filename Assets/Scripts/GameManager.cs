using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TarodevController; // Ensure PlayerController namespace is recognized

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI crispCounterText; // UI text for crisps
    public GameObject player; // Player GameObject
    public Transform playerStartPosition; // Player's starting position
    public CanvasGroup retryPanelCanvasGroup; // Retry panel for fading
    public Button retryButton; // Retry button
    public float fadeDuration = 1f; // Duration for fading
    private int crispsCollected = 0; // Current crisps collected
    public int totalCrispsNeeded = 30; // Total crisps to collect

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

    public void AddCrisp()
    {
        crispsCollected++;
        UpdateCrispCounter();

        if (crispsCollected >= totalCrispsNeeded)
        {
            Debug.Log("All crisps collected! You win!");
        }
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
        player.SetActive(true);

        // Reset player position
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
                // Reset position and reactivate
                seagulls[i].transform.position = seagullStartPositions[i];
                seagulls[i].SetActive(true);
            }
        }

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
