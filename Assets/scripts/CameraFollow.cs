using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // Assign the Player's Transform in the Inspector
    public float smoothSpeed = 0.125f; // Controls how smoothly the camera follows

    private Vector3 offset; // Keeps the camera at a fixed distance from the player

    void Start()
    {
        offset = transform.position - player.position; // Calculate the initial offset
    }

    void LateUpdate()
    {
        // Keeps the camera centered on the player while keeping the same offset
        Vector3 desiredPosition = player.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
    }
}
