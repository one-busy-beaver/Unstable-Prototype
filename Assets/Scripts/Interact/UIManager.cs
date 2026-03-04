using UnityEngine;
using System;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    // Callbacks for the "Press E" prompt
    public void ShowPrompt(string text) => Debug.Log($"UI Prompt: {text}");
    public void HidePrompt() => Debug.Log("Hide UI Prompt");

    // Callbacks for messages (e.g. "Locked!")
    public void ShowTimedMessage(string message) => Debug.Log($"UI Alert: {message}");

    // Callback for the Yes/No Panel
    public void ShowDecision(string message, Action onConfirm)
    {
        Debug.Log($"Decision: {message}. (Auto-confirming for now)");
        onConfirm?.Invoke(); // For now, we auto-confirm until you build the actual UI buttons
    }
}