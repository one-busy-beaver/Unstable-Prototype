using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class SceneExit : MonoBehaviour
{
    [Header("Transition Settings")]
    [SerializeField] private string sceneToLoad;
    [SerializeField] private string targetSpawnID;
    [SerializeField] private bool autoTrigger = true;

    [Header("Visuals (Editor Only)")]
    [SerializeField] private Color gizmoColor = new Color(0.65f, 0.89f, 0.34f, 0.5f); // #a6e356 with alpha


    // Ensure the collider is set to trigger so it doesn't block the player
    private void Awake() => GetComponent<BoxCollider2D>().isTrigger = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Only trigger automatically if autoTrigger is true
        if (autoTrigger && other.CompareTag("Player"))
        {
            ExecuteTransition();
        }
    }

    public void ExecuteTransition()
    {
        if (SceneLoader.Instance != null)
            SceneLoader.Instance.LoadScene(sceneToLoad, targetSpawnID);
        else
            Debug.LogError("SceneLoader missing!");
    }

    // ================================ Visualization ================================

    private void OnDrawGizmos()
    {
        BoxCollider2D box = GetComponent<BoxCollider2D>();
        if (box == null) return;

        // Capture current matrix and apply the object's transform
        Matrix4x4 oldMatrix = Gizmos.matrix;
        Gizmos.matrix = transform.localToWorldMatrix;

        Gizmos.color = gizmoColor;
        Gizmos.DrawCube(box.offset, box.size);

        Gizmos.color = new Color(gizmoColor.r, gizmoColor.g, gizmoColor.b, 1.0f);
        Gizmos.DrawWireCube(box.offset, box.size);

        // Reset matrix so other gizmos aren't affected
        Gizmos.matrix = oldMatrix;
    }
}