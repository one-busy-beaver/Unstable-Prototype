using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }
    public PlayerControls Controls { get; private set; }

    private const string RebindsKey = "InputRebinds";

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
        LoadRebinds(); // Load saved keys on startup
    }

    private void OnEnable()
    {
        // Use conditional access to prevent errors if Enable is called during setup
        Controls?.Enable();
    }

    private void OnDisable()
    {
        // The null-conditional operator (?.) prevents the NullReferenceException
        Controls?.Disable();
    }

    public void StartRebind(string actionName, int bindingIndex, System.Action onUIUpdate)
    {
        // Find the action (e.g., "Jump" or "Move")
        InputAction action = Controls.asset.FindAction(actionName);
        
        if (action == null || action.bindings.Count <= bindingIndex) return;

        // Disable controls during rebind to prevent ghost inputs
        action.Disable();

        var rebindOperation = action.PerformInteractiveRebinding(bindingIndex)
            .WithControlsExcluding("<Mouse>/leftButton") // Ignore the click used to start rebinding
            .OnMatchWaitForAnother(0.1f) // Short delay to prevent accidental double-inputs
            .OnComplete(operation =>
            {
                operation.Dispose();
                action.Enable();
                SaveRebinds();
                onUIUpdate?.Invoke(); // Trigger UI refresh
            })
            .Start();
    }

    private void SaveRebinds()
    {
        string rebinds = Controls.asset.SaveBindingOverridesAsJson();
        PlayerPrefs.SetString(RebindsKey, rebinds);
        PlayerPrefs.Save();
    }

    private void LoadRebinds()
    {
        string rebinds = PlayerPrefs.GetString(RebindsKey);
        if (!string.IsNullOrEmpty(rebinds))
        {
            Controls.asset.LoadBindingOverridesFromJson(rebinds);
        }
    }

    public string GetBindingDisplay(string actionName, int bindingIndex)
    {
        InputAction action = Controls.asset.FindAction(actionName);
        return action?.GetBindingDisplayString(bindingIndex) ?? "";
    }
}
