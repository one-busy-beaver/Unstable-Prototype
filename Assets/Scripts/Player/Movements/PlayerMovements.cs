using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    public static PlayerMovements Instance;
    public bool isMainPlayer;
    private Rigidbody2D rb;
    private Animator anim;
    private PlayerStates pState;
    private LineRenderer lineRenderer;


    [Header("Horizontal Movement Settings")]
    [SerializeField] private float walkSpeed = 8f;
    [SerializeField] private float dashSpeed = 20f;
    [SerializeField] private float dashTime = 0.15f;
    [SerializeField] private float dashCooldown = 0.3f;
    [SerializeField] private GameObject dashEffect;
    private float xAxis;
    private bool canDash = true;
    private bool dashed = false;
    private bool dashPressed;


    [Header("Vertical Movement Settings")]
    [SerializeField] private float jumpForce = 11f;
    [SerializeField] private float jumpBufferTime = 10f;
    [SerializeField] private float coyoteTime = 0.1f; 
    [SerializeField] private int airJumpCounter = 0;
    [SerializeField] private int maxAirJumps = 1;
    [SerializeField] private float maxFallSpeed = 15f;
    [SerializeField] private float fallMultiplier = 3f; // faster fall
    [SerializeField] private float riseMultiplier = 8f; // slower rise if jump released early
    private float jumpBufferCounter = 1f; // extend jump register before touching the ground
    private float coyoteTimeCounter = 0.1f; // extend jump register after leaving the ground
    private float gravity;
    private bool jumpPressed; // For the initial jump
    private bool jumpHeld; // For variable jump height


    [Header("Attacking Settings")]
    //[SerializeField] private Transform sideAttackTransform;
    //[SerializeField] private Transform upAttackTransform;
    //[SerializeField] private Transform downAttackTransform;
    [SerializeField] private Vector2 sideAttackArea = new Vector2(4.5f, 3.5f); 
    [SerializeField] private Vector2 upAttackArea = new Vector2(3.5f, 4.5f);
    [SerializeField] private Vector2 downAttackArea = new Vector2(3.5f, 4.5f);
    private bool attack = false;
    private float timeBetweenAttack;
    private float timeSinceAttack;


    [Header("Visualization Settings")]
    [SerializeField] bool visualize = true;
    [SerializeField] private int maxPoints = 15;  // how long the trail lasts
    [SerializeField] private float recordInterval = 0.05f;
    private float recordTimer;
    private List<Vector3> points = new List<Vector3>();

    // ================================================================================
    //                          Unity Lifecycle
    // ================================================================================

    void Awake()
    {
        if (!isMainPlayer)
        {
            Destroy(gameObject);
            return;
        }

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        pState = GetComponent<PlayerStates>();

        gravity = rb.gravityScale;

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0;
    }

    /*void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(sideAttackTransform.position, sideAttackArea);
        Gizmos.DrawWireCube(upAttackTransform.position, upAttackArea);
        Gizmos.DrawWireCube(downAttackTransform.position, downAttackArea);
    }*/

    void Update()
    {
        GetInputs();
        UpdateJumpVariables();
        if (!pState.isDashing)
        {
            Flip();
            if (!pState.isClimbing) Move();
        }
        if ((!pState.inWater || pState.onGround) && !pState.isClimbing) 
        {
            Jump();
        }
        StartDash();
        Attack();

        if (visualize) DrawTrail();
    }

    void GetInputs() 
    {
        Vector2 moveInput = InputManager.Instance.Controls.Player.Move.ReadValue<Vector2>();
        xAxis = moveInput.x;

        jumpPressed = InputManager.Instance.Controls.Player.Jump.triggered; 
        jumpHeld = InputManager.Instance.Controls.Player.Jump.IsPressed();

        dashPressed = InputManager.Instance.Controls.Player.Dash.triggered;

        // attack = InputManager.Instance.Controls.Player.Attack.IsPressed();
    }

    void FixedUpdate()
    {
        if (!pState.inWater && !pState.isClimbing) 
        {
            UpdateAirBorneState();
        }
    }

    // ================================================================================
    //                          Attack
    // ================================================================================

    void Attack()
    {
        timeSinceAttack += Time.deltaTime;

        if (attack && timeSinceAttack >= timeBetweenAttack) {
            timeSinceAttack = 0;
        }
    }
    
    // ================================================================================
    //                          Horizontal Movements
    // ================================================================================

    void Move()
    {
        rb.velocity = new Vector2(walkSpeed * xAxis, rb.velocity.y);
        anim.SetBool("Walking", rb.velocity.x != 0 && pState.onGround);
    }

    void StartDash()
    {
        if (dashPressed && canDash && !dashed)
        {
            StartCoroutine(Dash());
            dashed = true;
        }
        
        if (pState.onGround)
        {
            dashed = false;
        }
    }

    IEnumerator Dash()
    {
        canDash = false;
        pState.isDashing = true;
        anim.SetTrigger("Dashing");
        rb.gravityScale = 0;

        // Dash at the direction the character is facing
        rb.velocity = new Vector2(transform.localScale.x * dashSpeed, 0); 

        if (pState.onGround)
        {
            // Instanstiate dash effect as a child object
            Instantiate(dashEffect, transform); 
        }

        yield return new WaitForSeconds(dashTime);

        rb.gravityScale = gravity;
        pState.isDashing = false;

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
    //                          Vertical Movements
    // ================================================================================

    public void SetMaxAirJumps(int amount)
    {
        maxAirJumps = amount;
    }

    void Jump()
    {
        if (!pState.isJumping)
        {
            if (jumpBufferCounter > 0 && coyoteTimeCounter > 0)
            {
                // Jump
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);

                pState.isJumping = true;
            }
            else if (!pState.onGround && airJumpCounter < maxAirJumps && jumpPressed)
            {
                // Air jump
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                
                pState.isJumping = true;
                airJumpCounter++;
            }
        }

        if (!jumpHeld && rb.velocity.y > 0)
        {
            // Cancel jump (same line of code for released jump)
            rb.velocity += Vector2.up * Physics2D.gravity.y * (riseMultiplier - 1) * Time.fixedDeltaTime;

            pState.isJumping = false;
        }

        anim.SetBool("Jumping", !pState.onGround);
    }

    void UpdateJumpVariables()
    {
        if (pState.onGround)
        {
            pState.isJumping = false;
            coyoteTimeCounter = coyoteTime;
            airJumpCounter = 0;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (jumpPressed)
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
        else if (rb.velocity.y > 0 && !jumpHeld)
        {
            // Released jump: slower rise
            rb.velocity += Vector2.up * Physics2D.gravity.y * (riseMultiplier - 1) * Time.fixedDeltaTime;
        }
    }
    
    // ================================================================================
    //                          Visualization
    // ================================================================================

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
