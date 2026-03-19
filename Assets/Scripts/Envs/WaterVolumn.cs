using UnityEngine;

public class WaterVolume : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out PlayerSwim swim))
        {
            swim.EnterWater();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out PlayerSwim swim))
        {
            swim.ExitWater();
        }
    }
}