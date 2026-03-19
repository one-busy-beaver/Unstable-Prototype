using UnityEngine;

public class PlayerClimb : MonoBehaviour
{
    [Header("Climbing Settings")]
    [SerializeField] private float climbSpeed = 6f;
    [SerializeField] private Vector2 jumpOffForce = new Vector2(10f, 10f);
    
    [Header("Rope Sensor")]
    [SerializeField] private Transform ropeSensor;
    [SerializeField] private LayerMask ropeLayer;

    private Rigidbody2D rb;
    private PlayerStates pState;
    private float originalGravity;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pState = GetComponent<PlayerStates>();
        originalGravity = rb.gravityScale;
    }

    void Update()
    {
        // Read inputs using your existing InputManager
        Vector2 moveInput = InputManager.Instance.Controls.Player.Move.ReadValue<Vector2>();
        bool jumpPressed = InputManager.Instance.Controls.Player.Jump.triggered;

        // 1. Check if the sensor is touching a rope
        bool atRope = false;
        if (ropeSensor != null)
        {
            atRope = Physics2D.OverlapPoint(ropeSensor.position, ropeLayer);
        }

        // 2. Attach to rope (Ignore unless pressing UP)
        if (atRope && !pState.isClimbing && moveInput.y > 0.5f)
        {
            pState.isClimbing = true;
            rb.velocity = Vector2.zero; // Kill horizontal momentum
            rb.gravityScale = 0f; // Disable gravity so player doesn't slide down
            
            // Optional: Snap player X position to the rope's center here if desired
        }

        // 3. Handle Active Climbing
        if (pState.isClimbing)
        {
            // Climb up and down
            rb.velocity = new Vector2(0, moveInput.y * climbSpeed);

            // 4. Jump off the rope
            if (jumpPressed)
            {
                DetachFromRope();
                
                // Jump in the direction the character is currently facing
                float faceDirection = transform.localScale.x;
                rb.velocity = new Vector2(faceDirection * jumpOffForce.x, jumpOffForce.y);
            }

            // 5. Naturally fall off if player climbs past the top or bottom of the rope
            if (!atRope)
            {
                DetachFromRope();
            }
        }
    }

    private void DetachFromRope()
    {
        pState.isClimbing = false;
        rb.gravityScale = originalGravity; // Restore gravity
    }
}