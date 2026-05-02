using UnityEngine;
using System.Collections.Generic;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attacking Settings")]
    
    [SerializeField] PolygonCollider2D attackHitbox;
    [SerializeField] LayerMask attackableLayer;
    [SerializeField] float damage = 1.5f;
    [SerializeField] float timeBetweenAttack = 0.5f; // prevent infinite damage

    bool attack = false;    
    float timeSinceAttack;

    // Player components
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        attack = InputManager.Instance.Controls.Player.Attack.IsPressed();
        Attack();
    }

    void Attack()
    {
        timeSinceAttack += Time.deltaTime;

        if (attack && timeSinceAttack >= timeBetweenAttack) {
            timeSinceAttack = 0;
            anim.SetTrigger("Attacking");

            ProcessHit();
        }
    }

    void ProcessHit()
    {
        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(attackableLayer);
        filter.useLayerMask = true;
        filter.useTriggers = true;

        List<Collider2D> results = new List<Collider2D>();
        int hitCount = attackHitbox.Overlap(filter, results);

        if (hitCount > 0) Debug.Log("I'm hitting something");

        // Get the Player's own Recoil component
        Recoil playerRecoil = GetComponent<Recoil>();

        foreach (Collider2D curObj in results)
        {
            Enemy enemy = curObj.GetComponent<Enemy>();
            if (enemy != null)
            {
                // 1. Calculate the attack direction relative to player facing
                // If your player faces right, dir is (1, 0).
                Vector2 attackDir = new Vector2(transform.localScale.x, 0);

                // 2. Trigger Enemy Recoil (moves with the hit)
                if (curObj.TryGetComponent(out Recoil enemyRecoil))
                    enemyRecoil.TriggerRecoil(attackDir);

                // 3. Trigger Player Recoil (moves opposite to the hit)
                if (playerRecoil != null)
                    playerRecoil.TriggerRecoil(-attackDir);

                enemy.EnemyHit(damage);
            }
        }
    }
}