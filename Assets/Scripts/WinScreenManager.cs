using UnityEngine;
using UnityEngine.SceneManagement; // For scene management

public class WinScreenManager : MonoBehaviour
{
    public void RestartGame()
    {
        // Reload the first level or main gameplay scene
        SceneManager.LoadScene("AberdeenLevel"); // Replace with your level's name
    }

    public void QuitGame()
    {
        // Quit the application
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}
