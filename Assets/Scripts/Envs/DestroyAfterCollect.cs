using UnityEngine;

public class DestroyAfterCollect : MonoBehaviour 
{
    public CollectID uniqueID;

    void Awake() {
        // Check if this specific item was already picked up
        if (WorldState.Instance.IsCollected(uniqueID)) {
            Destroy(gameObject);
        }
    }

    public void OnPickUp() {
        WorldState.Instance.MarkAsCollected(uniqueID);
        // Rest of your pick-up logic...
        Destroy(gameObject);
    }
}