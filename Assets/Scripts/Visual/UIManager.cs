using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Decision Panel")]
    [SerializeField] private GameObject decisionPanel;
    [SerializeField] private TextMeshProUGUI decisionText;
    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;

    private void Awake()
    {
        if (Instance == null) Instance = this;

        // Hide everything on start
        if (decisionPanel != null) decisionPanel.SetActive(false);
    }

    // Callbacks for messages (e.g. "Locked!")
    public void ShowTimedMessage(string message) => Debug.Log($"UI Alert: {message}");

    // Updated to handle 'Yes' button state and button listeners
    public void ShowDecision(string message, bool canConfirm, Action onConfirm)
    {
        if (decisionPanel == null) return;

        decisionText.text = message;
        
        // Disable or enable the Yes button based on if the player has the key
        yesButton.interactable = canConfirm;

        // Clear previous listeners to prevent multiple calls
        yesButton.onClick.RemoveAllListeners();
        noButton.onClick.RemoveAllListeners();

        // Assign new listeners
        yesButton.onClick.AddListener(() => 
        {
            onConfirm?.Invoke();
            HideDecision();
        });

        noButton.onClick.AddListener(HideDecision);

        decisionPanel.SetActive(true);
    }

    public void HideDecision()
    {
        if (decisionPanel != null) decisionPanel.SetActive(false);
    }
}