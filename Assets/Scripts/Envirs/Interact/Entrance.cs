using UnityEngine;
using TMPro;

public class Entrance : Interactable
{
    [SerializeField] private string requiredKey;
    [SerializeField] private SceneExit sceneExit;

    public override void Interact()
    {
        bool hasKey = string.IsNullOrEmpty(requiredKey) || Inventory.Instance.HasItem(requiredKey);

        if (useDecisionPanel)
        {
            // Show the panel. Pass 'hasKey' so the UI knows if "Yes" should be clickable.
            UIManager.Instance.ShowDecision(decisionMessage, hasKey, ConfirmInteraction);
        }
        else
        {
            // Fallback for doors that don't use the UI panel
            if (hasKey)
            {
                ConfirmInteraction(); // Instantly go to next scene
            }
            else
            {
                UIManager.Instance.ShowTimedMessage($"It's locked. Needs {requiredKey}.");
            }
        }
    }

    public override void ConfirmInteraction()
    {
        sceneExit.ExecuteTransition();
    }
}