using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject instructionsPanel; // Reference to the InstructionsPanel
    public GameObject creditsPanel; // Reference to the CreditsPanel

    public TMP_InputField usernameInput;

    // private const string usernameKey = "PlayerUsername";

    public void PlayGame()
    {
        // Load the first level
        // Load previous username if available
        string username = usernameInput.text.Trim(); // Get input text and trim spaces

        if (string.IsNullOrEmpty(username))
        {
            username = "Player"; // Default name if empty
        }

        PlayerPrefs.SetString("PlayerUsername", username);
        PlayerPrefs.Save();

        Debug.Log("Username saved: " + username); // Debug log
        SceneManager.LoadScene("AberdeenLevel");
    }

    public void ShowInstructions()
    {
        // Activate the instructions panel
        instructionsPanel.SetActive(true);
    }

    public void HideInstructions()
    {
        // Deactivate the instructions panel
        instructionsPanel.SetActive(false);
    }

    public void ShowCredits()
    {
        creditsPanel.SetActive(true);
    }

    public void HideCredits()
    {
        creditsPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
