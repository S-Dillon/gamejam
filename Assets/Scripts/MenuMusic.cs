using UnityEngine;
using System.Collections;

public class MenuMusic : MonoBehaviour
{
    private static MenuMusic instance; // Singleton instance
    private AudioSource audioSource;

    private void Awake()
    {
        // Ensure only one instance of MenuMusic exists
        if (instance != null)
        {
            Destroy(gameObject); // Destroy duplicate music
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject); // Persist across scenes
    }

    private void Start()
    {
        // Get or add an AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Configure the AudioSource
        audioSource.volume = 0.1f; // Set the volume to 0.1
        audioSource.loop = true;   // Loop the menu music
        audioSource.Play();        // Start playing the music
    }

    public IEnumerator FadeOutMusic(float fadeDuration)
    {
        if (audioSource == null) yield break;

        float startVolume = audioSource.volume;

        // Gradually lower the volume to 0 over the fadeDuration
        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }

        // Stop the music after fading out
        audioSource.Stop();
        audioSource.volume = startVolume; // Reset the volume
    }
}
