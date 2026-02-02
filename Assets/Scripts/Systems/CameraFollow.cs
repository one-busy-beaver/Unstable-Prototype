using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public static CameraFollow Instance;
    private GameObject targetObject;

    [Header("Follow")]
    [SerializeField] private float followSpeed = 5f;
    [SerializeField] private Vector3 offset = new Vector3(0, 0, -10f);

    [Header("Zoom")]
    [SerializeField] float zoomSpeed = 5f;
    [SerializeField] float minZoom = 3f;
    [SerializeField] float maxZoom = 8f;
    private float targetZoom;

    [Header("Pan")]
    [SerializeField] float panSpeed = 8f;
    [SerializeField] float recenterSpeed = 8f;


    [Header("Player Bounds")]
    [SerializeField] float maxHorizontalOffset = 3f;
    [SerializeField] float maxVerticalOffset = 2f;

    Camera cam;
    Vector2 manualOffset;
    

    void Awake()
    {
        // Only allow one CameraFollow to exist
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        cam = GetComponent<Camera>();

        // Set targetZoom to the camera's actual size so it doesn't start at 0
        targetZoom = cam.orthographicSize;
    }

    void Start()
    {
        // If the camera wakes up and has no target, try to find one immediately
        if (targetObject == null)
        {
            GameObject existingPlayer = GameObject.FindWithTag("Player");
            if (existingPlayer != null)
            {
                SetTarget(existingPlayer);
            }
        }
    }

    void Update()
    {
        HandleZoom();
        HandlePan();
    }

    void LateUpdate()
    {
        FollowPlayer();
    }

    public void SetTarget(GameObject newTarget)
    {
        targetObject = newTarget;
    }

    void HandleZoom()
    {
        float zoom = InputManager.Instance.Controls.Camera.Zoom.ReadValue<float>();

        if (Mathf.Abs(zoom) > 0.01f)
        {
            // Update the target based on input
            targetZoom -= zoom * zoomSpeed * Time.deltaTime;
            targetZoom = Mathf.Clamp(targetZoom, minZoom, maxZoom);
        }

        // Smoothly transition the actual camera size to the target
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, Time.deltaTime * zoomSpeed * 10f);
    }

    void HandlePan()
    {
        Vector2 input = InputManager.Instance.Controls.Camera.Pan.ReadValue<Vector2>();

        // Handle X axis independently
        if (Mathf.Abs(input.x) > 0.01f)
        {
            manualOffset.x += input.x * panSpeed * Time.deltaTime;
        }
        else
        {
            manualOffset.x = Mathf.Lerp(manualOffset.x, 0, recenterSpeed * Time.deltaTime);
        }

        // Handle Y axis independently
        if (Mathf.Abs(input.y) > 0.01f)
        {
            manualOffset.y += input.y * panSpeed * Time.deltaTime;
        }
        else
        {
            manualOffset.y = Mathf.Lerp(manualOffset.y, 0, recenterSpeed * Time.deltaTime);
        }

        manualOffset.x = Mathf.Clamp(manualOffset.x, -maxHorizontalOffset, maxHorizontalOffset);
        manualOffset.y = Mathf.Clamp(manualOffset.y, -maxVerticalOffset, maxVerticalOffset);
    }

    void FollowPlayer()
    {
        // Ensure PlayerMovements.Instance exists before accessing
        if (targetObject == null) return;

        Vector3 target =
            targetObject.transform.position +
            offset +
            (Vector3)manualOffset;

        transform.position = Vector3.Lerp(
            transform.position,
            new Vector3(target.x, target.y, transform.position.z),
            followSpeed * Time.deltaTime
        );
    }
}
