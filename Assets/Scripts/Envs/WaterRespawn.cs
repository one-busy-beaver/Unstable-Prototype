using UnityEngine;

public class WaterRespawn : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Try to find the tracker on the player
            LastSafeGround tracker = other.GetComponent<LastSafeGround>();

            Debug.Log("on trigger enter");

            if (tracker != null)
            {
                Respawn(other.gameObject, tracker.GetLastSafePosition());
            }
            else
            {
                // Fallback: If no tracker is found, stay at current position or 
                // handle logic for a default spawn point.
                Debug.LogWarning("Player missing LastSafeGround component!");
            }
        }
    }

    private void Respawn(GameObject player, Vector3 targetPosition)
    {
        player.transform.position = targetPosition;

        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
        }
    }
}