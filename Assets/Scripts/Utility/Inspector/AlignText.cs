using UnityEngine;
using TMPro;

public class AlignText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI localText;
    [SerializeField] private Collider2D colliderToFollow;
    [SerializeField] private float offsetHeight = 0.5f;

    private void OnValidate()
    {
        Align();
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
}