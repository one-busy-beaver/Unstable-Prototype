using UnityEngine;


public class AbilityPickup : InteractEvent
{
    [Header("Ability Settings")]
    [SerializeField] private AbilityType abilityToGrant;
    [SerializeField] private bool destroyOnPickup = true;
    [SerializeField] private DestroyAfterCollect col;

    public override void Execute()
    {
        if (PlayerAbilities.Instance != null)
        {
            // Unlocks the ability based on the string ID provided in the Inspector
            PlayerAbilities.Instance.Unlock(abilityToGrant);
            
            Debug.Log($"Unlocked ability: {abilityToGrant}");

            if (destroyOnPickup)
            {
                col.OnPickUp();
                Destroy(gameObject);
            }
        }
        else
        {
            Debug.LogError("PlayerAbilities instance not found in the scene!");
        }
    }
}