using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;

    private Rigidbody2D rb;
    private Animator anim;
    private PlayerStateList pState;
    private LineRenderer lineRenderer;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        pState = GetComponent<PlayerStateList>();

        gravity = rb.gravityScale;

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0;
    }


    // Update is called once per frame
    void Update()
    {
        GetInputs();
        UpdateJumpVariables();
        if (pState.dashing) return; // Movement won't be triggered if the player is dashing
        Flip();
        Move();
        Jump();
        StartDash();

        DrawTrail();
    }

    void FixedUpdate()
    {
        UpdateAirBorneState();
    }
    
    // ================================================================================
    //                              Horizontal Movements
    // ================================================================================

    [Header("Horizontal Movement Settings")]
    [SerializeField] private float walkSpeed = 8f;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashTime;
    [SerializeField] private float dashCooldown;
    private float xAxis;
    private bool canDash = true;
    private bool dashed = false;


    void GetInputs() 
    {
        xAxis = Input.GetAxisRaw("Horizontal");
    }

    void Move()
    {
        rb.velocity = new Vector2(walkSpeed * xAxis, rb.velocity.y);
        anim.SetBool("Walking", rb.velocity.x != 0 && Grounded());
    }

    void StartDash()
    {
        if (Input.GetButtonDown("Dash") && canDash && !dashed)
        {
            StartCoroutine(Dash());
            dashed = true;
        }
        
        if (Grounded())
        {
            dashed = false;
        }
    }

    IEnumerator Dash()
    {
        canDash = false;
        pState.dashing = true;
        anim.SetTrigger("Dashing");
        rb.gravityScale = 0;
        rb.velocity = new Vector2(transform.localScale.x * dashSpeed, 0); // dash at the direction the character is facing

        yield return new WaitForSeconds(dashTime);

        rb.gravityScale = gravity;
        pState.dashing = false;

        yield return new WaitForSeconds(dashCooldown);

        canDash = true;
    }

    void Flip()
    {
        if (xAxis < 0)
        {
            transform.localScale = new Vector2(-1, transform.localScale.y);
        }
        else if (xAxis > 0)
        {
            transform.localScale = new Vector2(1, transform.localScale.y);
        }
    }

    // ================================================================================
    //                              Vertical Movements
    // ================================================================================

    [Header("Vertical Movement Settings")]
    [SerializeField] private float jumpForce = 10f;
    private float jumpBufferCounter = 0f; // extend jump register before touching the ground
    [SerializeField] private float jumpBufferTime = 10f;
    private float coyoteTimeCounter = 0; // extend jump register after leaving the ground
    [SerializeField] private float coyoteTime = 0.1f; 
    private int airJumpCounter = 0;
    [SerializeField] private int maxAirJumps = 1;
    [SerializeField] private float maxFallSpeed = 15f;
    [SerializeField] private float fallMultiplier = 3f; // faster fall
    [SerializeField] private float riseMultiplier = 2f; // slower rise if jump released early
    private float gravity;

    void Jump()
    {
        if (!pState.jumping)
        {
            if (jumpBufferCounter > 0 && coyoteTimeCounter > 0)
            {
                // Jump
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);

                pState.jumping = true;
            }
            else if (!Grounded() && airJumpCounter < maxAirJumps && Input.GetButtonDown("Jump"))
            {
                // Air jump
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                
                pState.jumping = true;
                airJumpCounter++;
            }
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0)
        {
            // Cancel jump (same line of code for released jump)
            rb.velocity += Vector2.up * Physics2D.gravity.y * (riseMultiplier - 1) * Time.fixedDeltaTime;

            pState.jumping = false;
        }

        anim.SetBool("Jumping", !Grounded());
    }

    void UpdateJumpVariables()
    {
        if (Grounded())
        {
            pState.jumping = false;
            coyoteTimeCounter = coyoteTime;
            airJumpCounter = 0;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump"))
        {
            // set jump buffer to the max
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            // Decrease jump buffer by one every frame
            jumpBufferCounter -= Time.deltaTime * 10;
        }
    }

    void UpdateAirBorneState()
    {
        if (rb.velocity.y < 0)
        {
            if (Math.Abs(rb.velocity.y) < maxFallSpeed)
            {
                // Falling: increase gravity to fall faster
                rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
            }
            else
            {
                // Falling: reached the max fall speed
                rb.velocity = new Vector2(rb.velocity.x, -maxFallSpeed);
            }
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            // Released jump: slower rise
            rb.velocity += Vector2.up * Physics2D.gravity.y * (riseMultiplier - 1) * Time.fixedDeltaTime;
        }
    }

    // ================================================================================
    //                              Check Ground
    // ================================================================================

    [Header("Ground Check Settings")]
    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private float groundCheckY = 0.1f;
    [SerializeField] private float groundCheckX= 0.25f;
    [SerializeField] private LayerMask whatIsGround;

    public bool Grounded() 
    {
        if (
            Physics2D.Raycast(groundCheckPoint.position,
                              Vector2.down, groundCheckY, whatIsGround) 
            || Physics2D.Raycast(groundCheckPoint.position + new Vector3(groundCheckX, 0, 0),
                                 Vector2.down, groundCheckY, whatIsGround)
            || Physics2D.Raycast(groundCheckPoint.position + new Vector3(-groundCheckX, 0, 0),
                                 Vector2.down, groundCheckY, whatIsGround)
        )
        {
            return true;
        }
        return false;
    }
    
    // ================================================================================
    //                              Visualization
    // ================================================================================

    [Header("Visualization Settings")]
    [SerializeField] int maxPoints = 20;  // how long the trail lasts
    [SerializeField] float recordInterval = 0.05f;

    private float recordTimer;
    private List<Vector3> points = new List<Vector3>();

    void DrawTrail()
    {
        recordTimer += Time.deltaTime;

        if (recordTimer >= recordInterval)
        {
            recordTimer = 0f;
            RecordPosition();
        }
    }
    
    void RecordPosition()
    {
        points.Add(rb.position);

        // Limit trail length
        if (points.Count > maxPoints) {
            points.RemoveAt(0);
        }

        // Update the line
        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPositions(points.ToArray());
    }
}
