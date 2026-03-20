using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [Header("UI Settings")]
    public string promptText = "Interact"; 

    [Header("References")]
    [SerializeField] private GatedInteraction interaction;

    [Header("World Space UI")]
    [SerializeField] private GameObject localCanvas;
    [SerializeField] private TextMeshProUGUI localTextComponent;

    private void OnValidate()
    {
        FetchReferences();
    }

    private void Awake()
    {
        if (localCanvas != null) localCanvas.SetActive(false);
    }

    public void TogglePrompt(bool show)
    {
        if (localCanvas == null) return;
        
        if (show && localTextComponent != null)
        {
            localTextComponent.text = promptText;
        }
        
        localCanvas.SetActive(show);
    }

    public void Interact()
    {
        if (interaction != null)
        {
            interaction.Execute();
        }
    }

    void FetchReferences()
    {
        // 1. Find GatedInteraction on this object (or siblings if needed)
        if (interaction == null) interaction = GetComponent<GatedInteraction>();
        if (interaction == null) interaction = GetComponentInParent<Transform>().GetComponentInChildren<GatedInteraction>();

        // 2. Find Canvas in children
        if (localCanvas == null)
        {
            Canvas canvas = GetComponentInChildren<Canvas>(true);
            if (canvas != null) localCanvas = canvas.gameObject;
        }

        // 3. Find Text Component in children
        if (localTextComponent == null)
        {
            localTextComponent = GetComponentInChildren<TextMeshProUGUI>(true);
        }
    }
}