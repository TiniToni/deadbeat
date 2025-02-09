using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.InputSystem;

public class controller : MonoBehaviour
{
    public Rigidbody2D body;
    private Animator animator;

    float horizontal;
    float vertical;

    public float runSpeed = 20.0f;
    public float moveLimiter = 0.7f;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

    }

    private void FixedUpdate()
    {
        if (horizontal != 0 && vertical != 0)
        {
            horizontal *= moveLimiter;
            vertical *= moveLimiter;
        }
        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
    }
    private void OnMovement(InputValue value)
    {   
        if(horizontal != 0 || vertical != 0)
        { 
            animator.SetFloat("X", horizontal);
            animator.SetFloat("Y", vertical);
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }
}

