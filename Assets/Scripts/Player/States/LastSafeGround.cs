using UnityEngine;

public class LastSafeGround : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float updateInterval = 0.5f; // How often to save position

    private PlayerStates pState;
    private Vector3 lastSafePosition;
    private PlayerMovements playerMovements;
    private float timer;

    void Start()
    {
        playerMovements = GetComponent<PlayerMovements>();
        // Initialize with starting position
        lastSafePosition = transform.position;
        pState = GetComponent<PlayerStates>();

    }

    void Update()
    {
        timer += Time.deltaTime;

        // We only update the safe position if the player is grounded 
        // and a small interval has passed to avoid saving "cliff edge" frames.
        if (timer >= updateInterval)
        {
            if (playerMovements != null && pState.onGround)
            {
                lastSafePosition = transform.position;
            }
            timer = 0;
        }
    }

    public Vector3 GetLastSafePosition()
    {
        return lastSafePosition;
    }
}