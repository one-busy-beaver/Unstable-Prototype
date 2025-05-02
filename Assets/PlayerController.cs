using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Horizontal Movement Settings")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float walkSpeed = 1f;
    private float xAxis;


    [Header("Ground Check Settings")]
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private float groundCheckY = 0.2f;
    [SerializeField] private float groundCheckX= 0.5f;
    [SerializeField] private LayerMask whatIsGround;


    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        
    }


    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        GetInputs();
        Move();
        Jump();
    }


    /// <summary>
    /// 
    /// </summary>
    void GetInputs() {
        xAxis = Input.GetAxisRaw("Horizontal");
    }


    /// <summary>
    /// 
    /// </summary>
    void Move() {
        rb.velocity = new Vector2(walkSpeed * xAxis, rb.velocity.y);
    }


    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public bool Grounded() {
        if (Physics2D.Raycast(
                groundCheckPoint.position, 
                Vector2.down, 
                groundCheckY, 
                whatIsGround
            ) || Physics2D.Raycast(
                groundCheckPoint.position + new Vector3(groundCheckX, 0, 0), 
                Vector2.down, 
                groundCheckY, 
                whatIsGround
            ) || Physics2D.Raycast(
                groundCheckPoint.position + new Vector3(-groundCheckX, 0, 0), 
                Vector2.down, 
                groundCheckY, 
                whatIsGround
            )
        ) {
            return true;
        }
        return false;
    }


    /// <summary>
    /// 
    /// </summary>
    void Jump() {
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0) {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }

        if (Input.GetButtonDown("Jump") && Grounded()) {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }
}
