using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class AbilityPickup : GatedInteraction
{
    [Header("Ability Settings")]
    [SerializeField] private AbilityType abilityToGrant;

    [SerializeField] private bool destroyOnPickup = true;

    public override void Execute()
    {
        if (PlayerAbilities.Instance != null)
        {
            // Unlocks the ability based on the string ID provided in the Inspector
            PlayerAbilities.Instance.Unlock(abilityToGrant);
            
            Debug.Log($"Unlocked ability: {abilityToGrant}");

            if (destroyOnPickup)
            {
                if (transform.parent != null)
                {
                    Destroy(transform.parent.gameObject);
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
        else
        {
            Debug.LogError("PlayerAbilities instance not found in the scene!");
        }
    }
}