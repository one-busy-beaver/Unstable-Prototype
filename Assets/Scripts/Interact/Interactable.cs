using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [Header("UI Settings")]
    public string promptText = "Interact"; 
    public bool useDecisionPanel = false;
    [TextArea] public string decisionMessage = "Press E to Interact";

    public abstract void Interact(); // Standard interaction logic

    // Called when the decision panel "Yes" is clicked
    // Virtual so children (like LockedExit) can override it
    public virtual void ConfirmInteraction() { } 
}