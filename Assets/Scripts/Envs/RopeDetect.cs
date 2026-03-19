using UnityEngine;

public class RopeDetect : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the object entering the trigger is the player
        if (collision.CompareTag("Player"))
        {
            PlayerClimb climbScript = collision.GetComponent<PlayerClimb>();
            if (climbScript != null)
            {
                // Tell the player they can climb and pass this rope's transform for snapping
                climbScript.EnterRope(transform);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Check if the object exiting the trigger is the player
        if (collision.CompareTag("Player"))
        {
            PlayerClimb climbScript = collision.GetComponent<PlayerClimb>();
            if (climbScript != null)
            {
                // Tell the player they are out of range
                climbScript.LeaveRope();
            }
        }
    }
}