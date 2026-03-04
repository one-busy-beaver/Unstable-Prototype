using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class SceneExit : MonoBehaviour
{
    [Header("Transition Settings")]
    [SerializeField] private string sceneToLoad;
    [SerializeField] private string targetSpawnID;
    
    [Header("Game State")]
    [SerializeField] private bool goesToStartMenu = false;

    [Header("Visuals (Editor Only)")]
    [SerializeField] private Color gizmoColor = new Color(0.65f, 0.89f, 0.34f, 0.5f); // #a6e356 with alpha

    // Ensure the collider is set to trigger so it doesn't block the player
    private void Awake() => GetComponent<BoxCollider2D>().isTrigger = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Use the tag we agreed on for the player
        if (other.CompareTag("Player")) ExecuteTransition();
    }

    public void ExecuteTransition()
    {
        GameState.ShowThanksMessage = goesToStartMenu;
        if (SceneLoader.Instance != null)
            SceneLoader.Instance.LoadScene(sceneToLoad, targetSpawnID);
        else
            Debug.LogError("SceneLoader missing!");
    }

    private void OnDrawGizmos()
    {
        BoxCollider2D box = GetComponent<BoxCollider2D>();
        if (box == null) return;

        // Capture current matrix to reset it later
        Matrix4x4 oldMatrix = Gizmos.matrix;

        // Apply the object's transform to the Gizmos matrix
        Gizmos.matrix = transform.localToWorldMatrix;

        Gizmos.color = gizmoColor;
        // Draw relative to the local origin (0,0,0) plus the collider's offset
        Gizmos.DrawCube(box.offset, box.size);

        // Draw outline
        Gizmos.color = new Color(gizmoColor.r, gizmoColor.g, gizmoColor.b, 1.0f);
        Gizmos.DrawWireCube(box.offset, box.size);

        // Reset matrix so other gizmos aren't affected
        Gizmos.matrix = oldMatrix;
    }
}