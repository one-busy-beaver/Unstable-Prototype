using UnityEngine;
using TMPro;

public class SyncText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI localText;
    [SerializeField] private Collider2D colliderToFollow;
    [SerializeField] private float offsetHeight = 0.5f;

    private void OnValidate()
    {
        FetchReferences();
        Align();
        SyncPrompt();
    }

    void FetchReferences()
    {
        // 1. Find the parent root (the sprite gameobject)
        Transform root = transform.parent;
        if (root == null) return;

        // 2. Find the Text (it's inside the Canvas sibling/child)
        if (localText == null)
        {
            localText = root.GetComponentInChildren<TextMeshProUGUI>();
        }

        // 3. Find the Collider (it's on the Interaction sibling)
        if (colliderToFollow == null)
        {
            colliderToFollow = root.GetComponentInChildren<Collider2D>();
        }
    }

    void Align()
    {
        if (colliderToFollow == null || localText == null) return;

        // Get the world position of the top of the collider
        float topY = colliderToFollow.bounds.max.y;

        // Set text position to that Y + offset
        localText.transform.position = new Vector3(
            colliderToFollow.transform.position.x, 
            topY + offsetHeight, 
            colliderToFollow.transform.position.z
        );
    }

    void SyncPrompt()
    {
        if (localText == null) return;

        // Pulls the promptText string from the Interactable component on this object or its parent
        Interactable interactable = GetComponentInParent<Interactable>();
        if (interactable != null)
        {
            localText.text = interactable.promptText;
        }
    }
}