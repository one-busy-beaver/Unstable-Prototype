using UnityEngine;
using TMPro;

public class Entrance : Interactable
{
    [SerializeField] private string requiredKey;
    [SerializeField] private SceneExit sceneExit;

    public override void Interact()
    {
        bool hasKey = string.IsNullOrEmpty(requiredKey) || Inventory.Instance.HasItem(requiredKey);

        if (hasKey)
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