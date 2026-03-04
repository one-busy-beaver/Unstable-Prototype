using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    public static PlayerAbilities Instance;

    [Header("Unlocked Abilities")]
    [SerializeField] private bool _hasDash = false;
    [SerializeField] private bool _hasDoubleJump = false;

    //private bool _hasWallJump = false;

    // Public Getters
    public bool HasDash => _hasDash;
    public bool HasDoubleJump => _hasDoubleJump;

    private void Awake() { if (Instance == null) Instance = this; }

    // Call this from your ItemPickup script!
    public void Unlock(string abilityID)
    {
        switch (abilityID)
        {
            case "Dash": _hasDash = true; break;
            case "DoubleJump": _hasDoubleJump = true; break;
            //case "WallSlide": _hasWallJump = true; break;
            default: Debug.LogWarning($"Ability {abilityID} not found!"); break;
        }
    }
}