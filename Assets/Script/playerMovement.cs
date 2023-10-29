using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private BoxCollider2D collider;
    private SpriteRenderer sprite;
    private Animator animator;

    // serializefield -> so u can just edit value in unity (convinience)
    // can pass in the editor
    [SerializeField] private LayerMask jumpableGround;

    // set variables for the movements + dir
    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;

    private enum AnimationState { idle, running, jumping, falling }

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    { 
        // GetAxisRaw -> reset value to 0 immediately
        dirX = Input.GetAxisRaw("Horizontal"); // moving left and right
        rigidBody.velocity = new Vector2(dirX * moveSpeed, rigidBody.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded()) // get it from Input Manager from Project Settings
        // GetButtonDown -> only effective for "pressing"
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
        }

        UpdateAnimationState();
    }

    // set boundaries and check whether the character is grounded
    private bool IsGrounded()
    {
        // collider.bounds.center, collider.bounds.size -> size of "box"
        // checks if we are overlapping the jumpableground
        return Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    private void UpdateAnimationState()
    {
        AnimationState state;

        // going right
        if (dirX > 0f)
        {
            state = AnimationState.running;
            sprite.flipX = false;
        }
        // going left
        else if (dirX < 0)
        {
            state = AnimationState.running;
            sprite.flipX = true; // facing left side
        }
        else
        {
            // idle state
            state = AnimationState.idle;
            animator.SetBool("running", false);
        }

        // jumping
        if (rigidBody.velocity.y > .1f)
        {
            state = AnimationState.jumping;
        }
        // falling
        else if (rigidBody.velocity.y < -.1f)
        {
            state = AnimationState.falling;
        }
   
        // align with the animator where we use int state
        animator.SetInteger("state", (int)state);
    }
}
