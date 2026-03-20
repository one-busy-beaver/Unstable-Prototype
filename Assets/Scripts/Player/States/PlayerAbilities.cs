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
    public void Unlock(AbilityType ability)
    {
        switch (ability)
        {
            case AbilityType.Dash: _hasDash = true; break;
            case AbilityType.DoubleJump: _hasDoubleJump = true; break;
            case AbilityType.Swim: _hasSwim = true; break;
        }
    }
}