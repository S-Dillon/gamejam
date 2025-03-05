using UnityEngine;
using System.Collections;

public class LevelMusicManager : MonoBehaviour
{
    private AudioSource audioSource;

    private void Start()
    {
        // Stop and fade out the menu music
        MenuMusic menuMusic = FindObjectOfType<MenuMusic>();
        if (menuMusic != null)
        {
            StartCoroutine(menuMusic.FadeOutMusic(1.5f)); // Fade out menu music over 1.5 seconds
            Destroy(menuMusic.gameObject, 1.5f); // Destroy the menu music object after fade-out
        }

        // Get or add an AudioSource component for the level music
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Configure the level music
        audioSource.volume = 0;    // Start at 0 for fade-in
        audioSource.loop = true;  // Loop the level music
        audioSource.Play();       // Start playing the level music

        // Fade in the level music
        StartCoroutine(FadeInMusic(1.5f)); // Fade in over 1.5 seconds
    }

    public IEnumerator FadeInMusic(float fadeDuration)
    {
        float targetVolume = 0.1f; // Target volume

        // Gradually increase the volume to the target over the fadeDuration
        while (audioSource.volume < targetVolume)
        {
            audioSource.volume += Time.deltaTime / fadeDuration * targetVolume;
            yield return null;
        }

        audioSource.volume = targetVolume; // Ensure the volume is exactly 0.1
    }
}
