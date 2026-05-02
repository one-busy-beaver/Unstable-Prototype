using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance;

    [Header("Player Stats")]
    public int currentHealth = 5;
    public int maxHealth = 5;

    [Header("Unlocked Abilities")]
    [SerializeField] private bool _hasDash = false;
    [SerializeField] private bool _hasDoubleJump = false;
    [SerializeField] private bool _hasSwim = false;

    [Header("Unlocked Items")]
    [SerializeField] private bool _hasKey = false;

    // Public "Getters"
    public bool HasDash => _hasDash;
    public bool HasDoubleJump => _hasDoubleJump;
    public bool HasSwim => _hasSwim;
    public bool HasKey => _hasKey;

    private void Awake() 
    { 
        if (Instance == null) 
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Call this from your ItemPickup script
    public void Unlock(CollectID ability)
    {
        switch (ability)
        {
            case CollectID.Dash: _hasDash = true; break;
            case CollectID.DoubleJump: _hasDoubleJump = true; break;
            case CollectID.Swim: _hasSwim = true; break;
            case CollectID.Key: _hasKey = true; break;
        }
    }

    public void UpdateHealth(int amount)
    {
        // Clamp health between 0 and maxHealth
        // currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth)
        currentHealth += amount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        if (currentHealth < 0) currentHealth = 0;
    }
}