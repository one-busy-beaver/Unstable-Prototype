using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int currentHealth = 5;
    [SerializeField] int maxHealth = 5;
    Recoil recoil;

    void Awake() => recoil = GetComponent<Recoil>();

    void Update() => HandleDeath();

    void HandleDeath()
    {
        if (currentHealth <= 0) { /* Handle Player Death */ }
    }

    public void TakeDamage(int damage, Vector2 sourcePos)
    {
        currentHealth -= damage;
        recoil.TriggerRecoil(sourcePos);
    }
}