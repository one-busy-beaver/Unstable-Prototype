using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStates : MonoBehaviour
{
    [Header("Current States")]
    public bool isJumping = false;
    public bool isDashing = false;
    public bool inWater = false;
    public bool isSubmerged = false; // checked by head sensor
    public bool onGround = false; // checked by ground sensor

    [Header("Ground Sensor")]
    [SerializeField] private Transform groundSensor;
    [SerializeField] private float groundCheckY = 0.1f;
    [SerializeField] private float groundCheckX = 0.1f;
    [SerializeField] private LayerMask groundLayer;

    [Header("Water Sensor")]
    [SerializeField] private Transform headSensor;
    [SerializeField] private LayerMask waterLayer;

    void Update()
    {
        CheckGround();
        CheckSubmersion();
    }

    private void CheckGround()
    {
        onGround = Physics2D.Raycast(groundSensor.position, Vector2.down, groundCheckY, groundLayer) 
            || Physics2D.Raycast(groundSensor.position + new Vector3(groundCheckX, 0, 0), Vector2.down, groundCheckY, groundLayer)
            || Physics2D.Raycast(groundSensor.position + new Vector3(-groundCheckX, 0, 0), Vector2.down, groundCheckY, groundLayer);
    }

    private void CheckSubmersion()
    {
        if (inWater && headSensor != null)
        {
            isSubmerged = Physics2D.OverlapPoint(headSensor.position, waterLayer);
        }
        else
        {
            isSubmerged = false;
        }
    }
}