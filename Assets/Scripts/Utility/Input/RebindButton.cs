using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RebindButton : MonoBehaviour
{
    [Header("Settings")]
    public string actionName;      // e.g., "Jump" or "Move"
    public int bindingIndex;       // 0 for buttons, 1-4 for WASD
    
    [Header("UI References")]
    public TextMeshProUGUI actionLabel; // "Jump"
    public TextMeshProUGUI keyLabel;    // "Space"
    public Button selfButton;

    private void Start()
    {
        RefreshUI();
        selfButton.onClick.AddListener(DoRebind);
    }

    public void RefreshUI()
    {
        actionLabel.text = actionName;
        // Uses the helper from our InputManager
        keyLabel.text = InputManager.Instance.GetBindingDisplay(actionName, bindingIndex);
    }

    private void DoRebind()
    {
        keyLabel.text = "Press any key...";
        InputManager.Instance.StartRebind(actionName, bindingIndex, RefreshUI);
    }
}