using UnityEngine;

public class PlayerWalk : MonoBehaviour
{
    [Header("Walking Settings")]
    [SerializeField] private float walkSpeed = 8f;
    
    // Player components
    private Rigidbody2D rb;
    private Animator anim;
    private PlayerStates pState;

    // Private variables
    private float moveInputX;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        pState = GetComponent<PlayerStates>();
    }

    void Update()
    {
        moveInputX = InputManager.Instance.Controls.Player.Move.ReadValue<Vector2>().x;

        if (!pState.isDashing)
        {
            HandleTurn();
            if (!pState.isClimbing) HandleWalk();
        }
    }

    void HandleTurn()
    {
        if (moveInputX < 0)
        {
            transform.localScale = new Vector2(-1, transform.localScale.y);
        }
        else if (moveInputX > 0)
        {
            transform.localScale = new Vector2(1, transform.localScale.y);
        }
    }

    void HandleWalk()
    {
        rb.velocity = new Vector2(walkSpeed * moveInputX, rb.velocity.y);
        anim.SetBool("Walking", rb.velocity.x != 0 && pState.onGround);
    }
}