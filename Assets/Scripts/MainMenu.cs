using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject instructionsPanel; // Reference to the InstructionsPanel
    public GameObject creditsPanel; // Reference to the CreditsPanel

    public void PlayGame()
    {
        // Load the first level
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
