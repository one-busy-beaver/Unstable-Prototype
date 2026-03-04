using UnityEngine;
using System;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("UI Slots")]
    // These two lines create the "empty boxes" in the Unity Inspector
    [SerializeField] private GameObject interactionPrompt; 
    [SerializeField] private CanvasGroup messagePanelGroup;
    [SerializeField] private TextMeshProUGUI promptTextComponent;

    private void Awake()
    {
        if (Instance == null) Instance = this;

        // Hide everything on start
        if (interactionPrompt != null) interactionPrompt.SetActive(false);
        if (messagePanelGroup != null) messagePanelGroup.alpha = 0;
    }

    // This is called by PlayerInteraction when you walk into a door
    public void ShowPrompt(string text)
    {
        if (interactionPrompt == null) return;

        if (promptTextComponent != null) 
        {
            promptTextComponent.text = text;
        }

        interactionPrompt.SetActive(true);
    }
    public void HidePrompt()
    {
        if (interactionPrompt != null) interactionPrompt.SetActive(false);
    }

    // Callbacks for messages (e.g. "Locked!")
    public void ShowTimedMessage(string message) => Debug.Log($"UI Alert: {message}");

    // Callback for the Yes/No Panel
    public void ShowDecision(string message, Action onConfirm)
    {
        Debug.Log($"Decision: {message}. (Auto-confirming for now)");
        onConfirm?.Invoke(); // For now, we auto-confirm until you build the actual UI buttons
    }
}