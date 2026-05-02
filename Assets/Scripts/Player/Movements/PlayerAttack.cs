using UnityEngine;
using System.Collections.Generic;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attacking Settings")]
    
    [SerializeField] PolygonCollider2D attackHitbox;
    [SerializeField] LayerMask attackableLayer;
    [SerializeField] float damage = 1.5f;
    [SerializeField] float timeBetweenAttack = 0.5f; // prevent infinite damage
    [SerializeField] float hitForce = 100;

    bool attack = false;    
    float timeSinceAttack;

    // Player components
    Rigidbody2D rb;
    Animator anim;
    PlayerStates pState;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        pState = GetComponent<PlayerStates>();
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
        
        // This uses the Polygon Collider's exact shape
        int hitCount = attackHitbox.Overlap(filter, results);

        if (hitCount > 0)
        {
            foreach (Collider2D curObj in results)
            {
                Enemy enemy = curObj.GetComponent<Enemy>();
                if (enemy != null)
                {
                    // Calculate direction: (Target - Self) to push away
                    Vector2 hitDirection = (curObj.transform.position - transform.position).normalized;
                    enemy.EnemyHit(damage, hitDirection, hitForce);
                }
            }
        }
    }
}