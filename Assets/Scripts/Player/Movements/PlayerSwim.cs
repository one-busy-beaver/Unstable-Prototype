using UnityEngine;

public class PlayerSwim : MonoBehaviour
{
    [Header("Swimming Settings")]
    [SerializeField] private float swimSinkSpeed = 4f;
    [SerializeField] private float swimUpForce = 7f;
    [SerializeField] private float swimSubmergeTime = 7f;

    [Header("Drowning Settings")]
    [SerializeField] private float drownSinkSpeed = 2f;
    [SerializeField] private float noSwimUpForce = 0f;
    [SerializeField] private float drownSubmergeTime = 0.5f;

    [Header("Active Config")]
    [SerializeField] private float activeSinkSpeed;
    [SerializeField] private float activeSwimUpForce;
    [SerializeField] private float activeMaxSubmergeTime;
    
    // Player components
    private Rigidbody2D rb;
    private PlayerStates pState;
    private LastSafeGround tracker;

    // Private variables
    private float currentSubmergeTime;    
    private float originalGravity;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pState = GetComponent<PlayerStates>();
        tracker = GetComponent<LastSafeGround>();
        originalGravity = rb.gravityScale;
    }

    void Update()
    {
        if (!pState.inWater) 
        {
            currentSubmergeTime = 0f;
            return;
        }

        if (!pState.onGround)
        {
            HandleSwim();
        }

        if (pState.isSubmerged) 
            HandleDrown();
        else
            currentSubmergeTime = 0f;
    }

    private void HandleSwim()
    {
        UpdateActiveAbilities();

        // Use your existing InputManager
        if (InputManager.Instance.Controls.Player.Jump.triggered)
        {
            rb.velocity = new Vector2(rb.velocity.x, activeSwimUpForce);
        }
        else if (rb.velocity.y < -activeSinkSpeed)
        {
            // Clamp downward velocity so you sink slowly instead of dropping like a rock
            rb.velocity = new Vector2(rb.velocity.x, -activeSinkSpeed);
        }
    }

    public void UpdateActiveAbilities()
    {
        bool hasSwim = PlayerInventory.Instance != null && PlayerInventory.Instance.HasSwim;

        activeSinkSpeed = hasSwim ? swimSinkSpeed : drownSinkSpeed;
        activeSwimUpForce = hasSwim ? swimUpForce : noSwimUpForce;
        activeMaxSubmergeTime = hasSwim ? swimSubmergeTime : drownSubmergeTime;
    }

    private void HandleDrown()
    {
        currentSubmergeTime += Time.deltaTime;

        if (currentSubmergeTime >= activeMaxSubmergeTime)
        {
            DrownAndRespawn();
        }
    }

    public void EnterWater()
    {
        pState.inWater = true;
        currentSubmergeTime = 0f;
        
        // Dampen entry velocity and lower gravity so water feels heavy
        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f); 
        rb.gravityScale = originalGravity * 0.5f; 
    }

    public void ExitWater()
    {
        pState.inWater = false;
        currentSubmergeTime = 0f;
        rb.gravityScale = originalGravity; // Restore normal gravity
    }

    private void DrownAndRespawn()
    {
        if (tracker != null)
        {
            transform.position = tracker.GetLastSafePosition();
        }
        else
        {
            Debug.LogWarning("LastSafeGround missing!");
        }
        
        rb.velocity = Vector2.zero;
        ExitWater();
    }
}