using UnityEngine;

public class ItemPickup : Interactable
{
    [SerializeField] private string itemID;
    [SerializeField] private bool isAbility = false;

    public override void Interact()
    {
        Debug.Log($"Picked up: {itemID}");
        
        // 1. Add to Inventory (We'll make this next)
        Inventory.Instance.AddItem(itemID);

        // 2. If it's an ability, unlock it
        if (isAbility) 
        {
            PlayerAbilities.Instance.Unlock(itemID);
        }

        // 3. Destroy the object in the scene
        Destroy(gameObject);
    }
}