using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneExit : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private string targetSpawnID;
    [SerializeField] private Color gizmoColor = Color.green;

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
            SceneLoader.Instance.LoadScene(sceneToLoad, targetSpawnID);
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
