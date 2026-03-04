using UnityEngine;
using TMPro;

public class AlignText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI localText;
    [SerializeField] private Collider2D colliderToFollow;
    [SerializeField] private float offsetHeight = 1.0f;

    private void OnValidate()
    {
        Align();
    }

    void Align()
    {
        if (colliderToFollow == null || localText == null) return;
        // 1. Get the world position of the top of the collider
        float topY = colliderToFollow.bounds.max.y;

        // 2. Set text position to that Y + your offset
        // Note: If using Screen Space UI, this requires different logic. 
        // This assumes World Space UI or a Sprite-based TMP.
        localText.transform.position = new Vector3(
            colliderToFollow.transform.position.x, 
            topY + offsetHeight, 
            colliderToFollow.transform.position.z
        );
    }
}