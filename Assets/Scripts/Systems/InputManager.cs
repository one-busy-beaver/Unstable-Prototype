using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }
    public PlayerControls Controls { get; private set; }

    private void Awake()
    {
        // Only allow one InputManager to exist
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        Controls = new PlayerControls();
    }

    private void OnEnable() => Controls.Enable();
    private void OnDisable() => Controls.Disable();
}
