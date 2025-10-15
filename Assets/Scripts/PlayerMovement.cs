using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float walkSpeed = 8f;
    private float xAxis;
    private Animator anim;
    public static PlayerMovement Instance;

    [Header("Movement Settings")]
    [SerializeField] private float jumpForce = 10f;
    private float curJumpForce = 0f;
    [SerializeField] private float maxFallSpeed = 15f;
    [SerializeField] private float fallMultiplier = 3f;    // faster fall
    [SerializeField] private float riseMultiplier = 2f; // slower rise if jump released early

    [Header("Ground Check Settings")]
    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private float groundCheckY = 0.1f;
    [SerializeField] private float groundCheckX= 0.25f;
    [SerializeField] private LayerMask whatIsGround;


    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        } else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();

        //lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0;
    }


    // Update is called once per frame
    void Update()
    {
        GetInputs();
        Move();
        Jump();
        Flip();

        UpdateTrajectory();
    }

    void FixedUpdate()
    {
        AirBorneConstraint();
    }
    
    // ====================================================================
    // Movements
    // ====================================================================

    void GetInputs() 
    {
        xAxis = Input.GetAxisRaw("Horizontal");
    }

    void Move()
    {
        rb.velocity = new Vector2(walkSpeed * xAxis, rb.velocity.y);
        anim.SetBool("Walking", rb.velocity.x != 0 && Grounded());
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

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && Grounded())
        {
            // Jump
            curJumpForce = jumpForce;
            rb.velocity = new Vector2(rb.velocity.x, curJumpForce);
            curJumpForce = Math.Max(curJumpForce--, 0);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0)
        {
            // Cancel jump
            rb.velocity += Vector2.up * Physics2D.gravity.y * (riseMultiplier - 1) * Time.fixedDeltaTime;
        }

        anim.SetBool("Jumping", !Grounded());
    }

    void AirBorneConstraint()
    {
        if (rb.velocity.y < 0)
        {
            // Falling: increase gravity to fall faster
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;

        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            // Released jump: slower rise
            rb.velocity += Vector2.up * Physics2D.gravity.y * (riseMultiplier - 1) * Time.fixedDeltaTime;
        }

        // TODO: I'll need to set up a max fall speed as well
    }
    
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] int maxPoints = 15;  // how long the trail lasts
    [SerializeField] float recordInterval = 0.05f;

    private float recordTimer;
    private List<Vector3> points = new List<Vector3>();

    void UpdateTrajectory()
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
