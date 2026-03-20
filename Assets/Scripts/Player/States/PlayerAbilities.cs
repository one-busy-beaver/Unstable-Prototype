using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    public static PlayerAbilities Instance;

    [Header("Unlocked Abilities")]
    [SerializeField] private bool _hasDash = false;
    [SerializeField] private bool _hasDoubleJump = false;
    [SerializeField] private bool _hasSwim = false;

    // Public "Getters"
    public bool HasDash => _hasDash;
    public bool HasDoubleJump => _hasDoubleJump;
    public bool HasSwim => _hasSwim;

    private void Awake() { if (Instance == null) Instance = this; }

    // Call this from your ItemPickup script
    public void Unlock(string abilityID)
    {
        switch (abilityID)
        {
            case "Dash": _hasDash = true; break;
            case "DoubleJump": _hasDoubleJump = true; break;
            case "Swim": _hasSwim = true; break;
            default: Debug.LogWarning($"Ability {abilityID} not found!"); break;
        }
    }
}