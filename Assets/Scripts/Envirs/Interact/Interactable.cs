using UnityEngine;
using TMPro;

public abstract class Interactable : MonoBehaviour
{
    [Header("UI Settings")]
    public string promptText = "Interact"; 
    public bool useDecisionPanel = false;
    [TextArea] public string decisionMessage = "Press E to Interact";

    [Header("World Space UI")]
    [SerializeField] protected GameObject localCanvas;
    [SerializeField] protected TextMeshProUGUI localTextComponent;

    protected virtual void Awake()
    {
        if (localCanvas != null) localCanvas.SetActive(false);
    }

    public virtual void TogglePrompt(bool show)
    {
        if (localCanvas == null) return;
        
        if (show && localTextComponent != null)
        {
            localTextComponent.text = promptText;
        }
        
        localCanvas.SetActive(show);
    }

    public abstract void Interact(); // Standard interaction logic

    // Called when the decision panel "Yes" is clicked
    // Virtual so children (like LockedExit) can override it
    public virtual void ConfirmInteraction() { } 
}