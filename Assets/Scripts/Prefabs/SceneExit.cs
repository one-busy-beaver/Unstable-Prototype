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

    private void Awake()
    {
        // Ensure the collider is set to trigger so it doesn't block the player
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Use the tag we agreed on for the player
        if (other.CompareTag("Player"))
        {
            // Logic for end-game message
            GameState.ShowThanksMessage = goesToStartMenu;

            // Call the Singleton we set up in the Bootstrap scene
            if (SceneLoader.Instance != null)
            {
                SceneLoader.Instance.LoadScene(sceneToLoad, targetSpawnID);
            }
            else
            {
                Debug.LogError("SceneExit: No SceneLoader found! Are you starting from the _Bootstrap scene?");
            }
        }
    }

    private void OnDrawGizmos()
    {
        BoxCollider2D box = GetComponent<BoxCollider2D>();
        if (box != null)
        {
            Gizmos.color = gizmoColor;
            // Draw a solid cube so it's easier to see in the Scene view
            Vector3 center = transform.position + (Vector3)box.offset;
            Gizmos.DrawCube(center, box.size);
            
            // Draw a wireframe outline for clarity
            Gizmos.color = new Color(gizmoColor.r, gizmoColor.g, gizmoColor.b, 1.0f);
            Gizmos.DrawWireCube(center, box.size);
        }
    }
}