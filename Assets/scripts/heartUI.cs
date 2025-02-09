using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heartUI : MonoBehaviour
{
    
    public float normalSpeed = 1f;  // Speed of heartbeat animation in normal state
    public float dangerSpeed = 3f;  // Speed of heartbeat animation in danger state

    private Animator heartAnimator; // Animator for heartbeat animation

    private void Start()
    {
        // Ensure the heart image has an animator attached to it
        heartAnimator = GetComponent<Animator>(); // Correctly get the Animator from the heart GameObject

        if (heartAnimator == null)
        {
            Debug.LogError("No Animator component found on Heart Image!");
        }
    }

    // Method to set heartbeat speed
    public void SetHeartbeatSpeed(float speed)
    {
        if (heartAnimator != null)
        {
            heartAnimator.speed = speed;
        }
    }
}
