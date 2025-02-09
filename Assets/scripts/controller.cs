using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    public Rigidbody2D body;
    public Animator animator;  // Reference to the Animator component

    private float horizontal;
    private float vertical;

    public float runSpeed = 5.0f;  // Adjust speed as needed
    private Vector3 originalScale;  // Store original scale

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // Get Animator attached to GameObject
        originalScale = transform.localScale; // Save the original scale of the character
    }

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        bool isMoving = horizontal != 0 || vertical != 0;  
        animator.SetBool("isWalking", isMoving);  // Trigger walking animation when moving

        if (isMoving)
        {
            // Flip character but keep the original scale
            if (horizontal > 0)
                transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z); // Right
            else if (horizontal < 0)
                transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z); // Left
        }
    }

    private void FixedUpdate()
    {
        // Apply movement
        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
    }
}

