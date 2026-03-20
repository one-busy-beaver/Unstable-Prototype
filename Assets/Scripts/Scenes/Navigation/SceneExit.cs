using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class SceneExit : GatedInteraction
{
    [Header("Transition Settings")]
    [SerializeField] private SceneID sceneToLoad;
    [SerializeField] private SceneID exitedSceneID;

    [Header("Visuals (Editor Only)")]
    [SerializeField] private Color gizmoColor = new Color(0.65f, 0.89f, 0.34f, 0.5f); // #a6e356 with alpha

    public override void Execute()
    {
        ExecuteTransition();
    }

    void Reset()
    {
        autoTrigger = true;
    }

    private void Awake() => GetComponent<BoxCollider2D>().isTrigger = true; // Ensure collider is set to trigger
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (autoTrigger && other.CompareTag("Player"))
        {
            ExecuteTransition();
        }
    }

    public void ExecuteTransition()
    {
        if (SceneLoader.Instance != null)
            SceneLoader.Instance.LoadScene(sceneToLoad, exitedSceneID);
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