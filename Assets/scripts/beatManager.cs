using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatManager : MonoBehaviour
{
    public AudioSource normal;
    public AudioSource heightened;
    private bool isHeightened = false;
    private float fadeDuration = 1.5f;

    public heartUI heartUI;  // Reference to the HeartUI component (drag the HeartUI script here)

    void Start()
    {
        normal.Play();
        heightened.volume = 0;
        heightened.Play();
    }

    public void triggerHeightened()
{
    if (!isHeightened)
    {
        isHeightened = true;
        StartCoroutine(FadeHeartbeats(normal, heightened));

        // Ensure heartUI is assigned
        if (heartUI != null)
        {
            heartUI.SetHeartbeatSpeed(heartUI.dangerSpeed);  // Speed up the heartbeat
        }
        else
        {
            Debug.LogError("HeartUI reference is not assigned in BeatManager!");
        }
    }
}


    public void resetNormal()
    {
        if (isHeightened)
        {
            isHeightened = false;
            StartCoroutine(FadeHeartbeats(heightened, normal));

            if (heartUI != null)
            {
                heartUI.SetHeartbeatSpeed(heartUI.normalSpeed);  // Return heartbeat to normal speed
            }
        }
    }

    private IEnumerator FadeHeartbeats(AudioSource fadeOut, AudioSource fadeIn)
    {
        float startVol = fadeOut.volume;
        fadeIn.volume = 0;
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadeOut.volume = Mathf.Lerp(startVol, 0, timer / fadeDuration);
            fadeIn.volume = Mathf.Lerp(0, startVol, timer / fadeDuration);
            yield return null;
        }

        fadeOut.volume = 0;
        fadeIn.volume = startVol;
    }
}