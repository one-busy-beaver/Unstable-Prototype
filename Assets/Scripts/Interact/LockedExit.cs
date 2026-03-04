using UnityEngine;

public class LockedExit : Interactable
{
    [SerializeField] private string requiredKey;
    [SerializeField] private SceneExit sceneExit;

    public override void Interact()
    {
        // If we don't require a key, or if we have it
        if (string.IsNullOrEmpty(requiredKey) || Inventory.Instance.HasItem(requiredKey))
        {
            if (useDecisionPanel)
            {
                // Open UI with Yes/No
                UIManager.Instance.ShowDecision(decisionMessage, ConfirmInteraction);
            }
            else
            {
                ConfirmInteraction();
            }
        }
        else
        {
            UIManager.Instance.ShowTimedMessage($"It's locked. Needs {requiredKey}.");
        }
    }

    public override void ConfirmInteraction()
    {
        sceneExit.ExecuteTransition();
    }
}