using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string sceneToLoad;
    public Vector2 spawnPosition;
    public Color gizmoColor = Color.green;

    private void OnValidate()
    {
        if (ColorUtility.TryParseHtmlString("#a6e356", out gizmoColor)) 
        {
            // Assign the color to a material or UI element
            GetComponent<SpriteRenderer>().color = gizmoColor;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Stop Player Input
            PlayerMovements playerMove = other.GetComponent<PlayerMovements>(); 
            if (playerMove != null) playerMove.enabled = false;

            // Stop Physics
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.zero;
                rb.bodyType = RigidbodyType2D.Kinematic; // Prevents further gravity/physics
            }

            // Store Spawn Data
            PlayerPrefs.SetFloat("SpawnX", spawnPosition.x);
            PlayerPrefs.SetFloat("SpawnY", spawnPosition.y);

            // Load Scene
            SceneManager.LoadSceneAsync(sceneToLoad); // does not pause the current scene
        }
    }

    private void OnDrawGizmos()
    {

        BoxCollider2D box = GetComponent<BoxCollider2D>();
        if (box != null)
        {
            Gizmos.color = gizmoColor;
            // Draw a wireframe box matching the collider's size and offset
            Gizmos.DrawWireCube((Vector2)transform.position + box.offset, box.size);
        }
    }
}
