using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource normalMusic;
    public AudioSource dangerMusic;
    public AudioSource rhythmMusic1;
    public AudioSource rhythmMusic2;

    private bool isScared = false;
    private bool isRhythmActive = false;
    public float fadeDuration = 1.5f;
    private AudioSource currentRhythmMusic;

    // Start is called before the first frame update
    void Start()
    {
        normalMusic.Play();
        dangerMusic.volume = 0;
    }

    // Play danger music and fade it in
    public void PlayDangerMusic()
    {
        if (!isScared)
        {
            isScared = true;
            StartCoroutine(Fade(normalMusic, dangerMusic)); // Fade to danger music
        }
    }

    public void StopDangerMusic()
    {
        dangerMusic.Stop();
        dangerMusic.volume = 0;  // Reset volume to avoid popping sound
    }

    // Play rhythm music (after danger music) and fade it in
    public void PlayRhythmMusic()
    {
        if (!isRhythmActive && isScared)
        {
            isRhythmActive = true;
            currentRhythmMusic = (Random.Range(0, 2) == 0) ? rhythmMusic1 : rhythmMusic2;
            StartCoroutine(Fade(dangerMusic, currentRhythmMusic)); // Fade from danger to rhythm music
        }
    }
    // Stop danger music and fade back to normal music
    public void StopRhythmMusic()
    {
        if (isRhythmActive)
        {
            isRhythmActive = false;
            isScared = false;
            StartCoroutine(Fade(currentRhythmMusic, normalMusic)); // Fade to normal music
        }
    }

    // Coroutine to fade music
    private IEnumerator Fade(AudioSource fadeOut, AudioSource fadeIn)
    {
        float startingVol = fadeOut.volume; // Get the initial volume of the music to fade out
        float timer = 0f;

        fadeIn.Play();  // Start the new music
        fadeIn.volume = 0f;  // Set the new music volume to 0 to start the fade-in

        // Fade loop
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;  // Increment the timer by deltaTime

            fadeOut.volume = Mathf.Lerp(startingVol, 0f, timer / fadeDuration);  // Fade out the current music
            fadeIn.volume = Mathf.Lerp(0f, startingVol, timer / fadeDuration);  // Fade in the new music

            yield return null;  // Wait for the next frame
        }

        fadeOut.Stop();  // Stop the music that is fading out
        fadeOut.volume = startingVol;  // Restore the initial volume of the old music
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
