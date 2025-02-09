using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.InputSystem;
public CutSceneController cutSceneController;


public class CharacterController : MonoBehaviour
{
    public Rigidbody2D body;
    public Animator animator;
    public GameObject flashlight;

    private float horizontal;
    private float vertical;
    private Vector2 lastDirection = Vector2.right; // Store last movement direction

    public float runSpeed = 5.0f;
    private Vector3 originalScale;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        originalScale = transform.localScale;

        if (cutSceneController != null){
            cutSceneController.PlayCutscene();  
        }
    }

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        bool isMoving = horizontal != 0 || vertical != 0;
        animator.SetBool("isWalking", isMoving);

        // Store last movement direction when moving
        if (isMoving)
        {
            lastDirection = new Vector2(horizontal, vertical).normalized;
        }

        // Flip character sprite based on horizontal movement direction
        if (horizontal > 0)
        {
            // Face right
            transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);  
        }
        else if (horizontal < 0)
        {
            // Face left
            transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z); 
        }

        // Flip flashlight based on horizontal direction
        if (horizontal > 0)
        {
            // Flashlight points right (no flip needed)
            flashlight.transform.localScale = new Vector3(Mathf.Abs(flashlight.transform.localScale.x), flashlight.transform.localScale.y, flashlight.transform.localScale.z);
        }
        else if (horizontal < 0)
        {
            // Flip flashlight to point left
            flashlight.transform.localScale = new Vector3(-Mathf.Abs(flashlight.transform.localScale.x), flashlight.transform.localScale.y, flashlight.transform.localScale.z);
        }

        // Handle flashlight rotation based on last movement direction
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
        // Apply movement
        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
        flashlight.transform.position = transform.position;
    }
}

