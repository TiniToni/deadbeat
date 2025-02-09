using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.InputSystem;


public class CharacterController : MonoBehaviour
{
    public Transform mapTransform; // Assign the map's transform in the Inspector
    public Animator animator;
    public GameObject flashlight;

    private float horizontal;
    private float vertical;
    private Vector2 lastDirection = Vector2.right;

    public float moveSpeed = 5.0f;
    private SpriteRenderer spriteRenderer;  // Add a reference to the SpriteRenderer


    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();  // Get the SpriteRenderer component

    }

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        bool isMoving = horizontal != 0 || vertical != 0;
        animator.SetBool("isWalking", isMoving);


        if (isMoving)
        {
            lastDirection = new Vector2(horizontal, vertical).normalized;
        }

         if (horizontal > 0)
        {
            spriteRenderer.flipX = false;  // Character is facing right
        }
        else if (horizontal < 0)
        {
            spriteRenderer.flipX = true;   // Character is facing left
        }

        // Rotate flashlight based on movement direction
        if (lastDirection.x > 0)
        {
            flashlight.transform.rotation = Quaternion.Euler(0, 0, 0);  // Point right
        }
        else if (lastDirection.x < 0)
        {
            flashlight.transform.rotation = Quaternion.Euler(0, 0, 180); // Point left
        }
        else if (lastDirection.y > 0)
        {
            flashlight.transform.rotation = Quaternion.Euler(0, 0, 90);  // Point up
        }
        else if (lastDirection.y < 0)
        {
            flashlight.transform.rotation = Quaternion.Euler(0, 0, -90);  // Point down
        }
    }

    private void FixedUpdate()
    {
        // Instead of moving the player, move the map
        mapTransform.position -= new Vector3(horizontal, vertical, 0) * moveSpeed * Time.fixedDeltaTime;
    }
    
}
