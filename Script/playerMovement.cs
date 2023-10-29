using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    // private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator animator;

    // [SerializeField] private LayerMask jumpableGround;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;

    private enum AnimationState { idle, running, jumping, falling }

    // [SerializeField] private AudioSource jumpSoundEffect;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        // coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    { 
        // GetAxisRaw -> reset value to 0 immediately
        dirX = Input.GetAxisRaw("Horizontal");
        rigidBody.velocity = new Vector2(dirX * moveSpeed, rigidBody.velocity.y);
        // dirX = Input.GetAxisRaw("Horizontal");
        // rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        // if (Input.GetButtonDown("Jump") && IsGrounded())
        if (Input.GetButtonDown("Jump")) // get it from Input Manager from Project Settings
        // GetButtonDown -> only effective for "pressing"
        {
        //     jumpSoundEffect.Play();
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
        }
        
        
        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        if (dirX > 0f)
        {
            animator.SetBool("running", true);
            sprite.flipX = false;
        }
        else if (dirX < 0) {
            animator.SetBool("running", true);
            sprite.flipX = true;
        }
        else {
            animator.SetBool("running", false);
        }
    //     MovementState state;

    //     if (dirX > 0f)
    //     {
    //         state = MovementState.running;
    //         sprite.flipX = false;
    //     }
    //     else if (dirX < 0f)
    //     {
    //         state = MovementState.running;
    //         sprite.flipX = true;
    //     }
    //     else
    //     {
    //         state = MovementState.idle;
    //     }

    //     if (rb.velocity.y > .1f)
    //     {
    //         state = MovementState.jumping;
    //     }
    //     else if (rb.velocity.y < -.1f)
    //     {
    //         state = MovementState.falling;
    //     }

    //     anim.SetInteger("state", (int)state);
    // }

    // private bool IsGrounded()
    // {
    //     return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
