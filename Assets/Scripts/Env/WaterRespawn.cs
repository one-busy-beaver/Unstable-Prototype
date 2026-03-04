using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterRespawn : MonoBehaviour
{
    // Drag your spawn point transform here in the Inspector
    [SerializeField] Transform spawnPoint;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object entering the water is the Player
        if (other.CompareTag("Player"))
        {
            Respawn(other.gameObject);
        }
    }

    private void Respawn(GameObject player)
    {
        // Move the player to the spawn point position
        player.transform.position = spawnPoint.position;

        // Reset velocity if the player has a Rigidbody2D to prevent carrying momentum
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
        }
    }
}
