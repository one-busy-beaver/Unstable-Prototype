using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class InteractHandler : MonoBehaviour
{
    [Header("UI Settings")]
    [SerializeField] private string promptText = "Interact"; 
    [SerializeField] private float offsetHeight = 0.5f;

    [Header("References")]
    [SerializeField] private InteractEvent interaction;
    

    [Header("World Space UI")]
    [SerializeField] private GameObject localCanvas;
    [SerializeField] private TextMeshProUGUI localTextComponent;
    [SerializeField] private Collider2D colliderToFollow;

    private void OnValidate()
    {
        FetchReferences();
        SyncUI();
    }

    private void Awake()
    {
        if (localCanvas != null) localCanvas.SetActive(false);
    }

    public void TogglePrompt(bool show)
    {
        if (localCanvas == null) return;
        
        if (show) SyncUI();
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
        // Find GatedInteraction on this object (or siblings if needed)
        if (interaction == null) interaction = GetComponent<InteractEvent>();

        // Find Canvas in children
        if (localCanvas == null)
        {
            Canvas canvas = GetComponentInChildren<Canvas>(true);
            if (canvas != null) localCanvas = canvas.gameObject;
        }

        // Find Text Component in children
        if (localTextComponent == null)
        {
            localTextComponent = GetComponentInChildren<TextMeshProUGUI>(true);
        }

        // Find Collider for alignment
        if (colliderToFollow == null)
        {
            colliderToFollow = GetComponent<Collider2D>() ?? GetComponentInChildren<Collider2D>();
        }
    }

    private void SyncUI()
    {
        if (localTextComponent == null) return;

        // Update Text
        localTextComponent.text = promptText;

        // Update Position based on collider bounds
        if (colliderToFollow != null)
        {
            float topY = colliderToFollow.bounds.max.y;
            localTextComponent.transform.position = new Vector3(
                colliderToFollow.transform.position.x,
                topY + offsetHeight,
                colliderToFollow.transform.position.z
            );
        }
    }
}