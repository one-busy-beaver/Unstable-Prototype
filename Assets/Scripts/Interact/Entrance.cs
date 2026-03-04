using UnityEngine;
using TMPro;

public class Entrance : Interactable
{
    [SerializeField] private string requiredKey;
    [SerializeField] private SceneExit sceneExit;

    [Header("Local UI")]
    [SerializeField] private GameObject localCanvas; // The "Canvas" child
    [SerializeField] private TextMeshProUGUI localText; // The "InWorldPrompt" child

    private void Awake()
    {
        // Hide UI by default so it's not always visible
        if (localCanvas != null) localCanvas.SetActive(false);
    }

    public void TogglePrompt(bool show)
    {
        if (localCanvas == null) return;
        
        if (show && localText != null)
        {
            localText.text = promptText; // Sets "New Text" to "Press E to Enter"
        }
        
        localCanvas.SetActive(show);
    }

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