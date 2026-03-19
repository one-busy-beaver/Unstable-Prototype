using UnityEngine;

public class PlayerSwim : MonoBehaviour
{
    [Header("Sensors")]
    [SerializeField] private Transform headSensor;
    [SerializeField] private LayerMask waterLayer;
    private bool isSubmerged;

    [Header("Configs With Swim")]
    [SerializeField] private float swimSinkSpeed = 4f;
    [SerializeField] private float swimUpForce = 7f;
    [SerializeField] private float swimSubmergeTime = 7f;

    [Header("Configs Without Swim")]
    [SerializeField] private float drownSinkSpeed = 1f;
    [SerializeField] private float noSwimUpForce = 0f;
    [SerializeField] private float drownSubmergeTime = 1f;

    [Header("Active Configs")]
    [SerializeField] private float activeSinkSpeed;
    [SerializeField] private float activeSwimUpForce;
    [SerializeField] private float activeMaxSubmergeTime;
    
    private float currentSubmergeTime;
    private Rigidbody2D rb;
    private PlayerStateList pState;
    private LastSafeGround tracker;
    private float originalGravity;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pState = GetComponent<PlayerStateList>();
        tracker = GetComponent<LastSafeGround>();
        originalGravity = rb.gravityScale;
    }

    void Update()
    {
        UpdateActiveAbilities();

        if (!pState.inWater) 
        {
            currentSubmergeTime = 0f;
            isSubmerged = false;
            return;
        }

        if (!pState.onGround)
        {
            HandleSwimming();
        }

        isSubmerged = Physics2D.OverlapPoint(headSensor.position, waterLayer);

        if (isSubmerged) 
            HandleDrowning();
        else
            currentSubmergeTime = 0f;
    }

    public void UpdateActiveAbilities()
    {
        bool hasSwim = PlayerAbilities.Instance != null && PlayerAbilities.Instance.HasSwim;

        activeSinkSpeed = hasSwim ? swimSinkSpeed : drownSinkSpeed;
        activeSwimUpForce = hasSwim ? swimUpForce : noSwimUpForce;
        activeMaxSubmergeTime = hasSwim ? swimSubmergeTime : drownSubmergeTime;
    }

    private void HandleSwimming()
    {
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

    private void HandleDrowning()
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