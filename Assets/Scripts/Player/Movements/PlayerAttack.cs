using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attacking Settings")]
    [SerializeField] Transform sideAttackTransform;
    [SerializeField] Transform upAttackTransform;
    [SerializeField] Transform downAttackTransform;

    [SerializeField] Vector2 sideAttackArea = new Vector2(4.5f, 3.5f); 
    [SerializeField] Vector2 upAttackArea = new Vector2(3.5f, 4.5f);
    [SerializeField] Vector2 downAttackArea = new Vector2(3.5f, 4.5f);

    [SerializeField] LayerMask attackableLayer;
    [SerializeField] float damage = 1.5f;
    [SerializeField] float timeBetweenAttack = 0.5f; // prevent infinite damage
    [SerializeField] float hitForce = 100;

    [SerializeField] GameObject slashEffect;

    bool attack = false;
    float moveInputY;
    
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
        moveInputY = InputManager.Instance.Controls.Player.Move.ReadValue<Vector2>().y;
        Attack();
    }

    void Attack()
    {
        timeSinceAttack += Time.deltaTime;

        if (attack && timeSinceAttack >= timeBetweenAttack) {
            timeSinceAttack = 0;
            anim.SetTrigger("Attacking");

            // side way attack
            if (moveInputY == 0 || moveInputY < 0 && pState.onGround)
            {
                Hit(sideAttackTransform, sideAttackArea);
                Instantiate(slashEffect, sideAttackTransform);
            } 
            // up attack
            else if (moveInputY > 0)
            {
                Hit(upAttackTransform, upAttackArea);
                SlashEffectAtAngle(slashEffect, 90, upAttackTransform);
            }
            // down attack
            else if (moveInputY < 0 && !pState.onGround)
            {
                Hit(downAttackTransform, downAttackArea);
                SlashEffectAtAngle(slashEffect, -90, downAttackTransform);
            }
        }
    }

    void Hit(Transform _attackTransform, Vector2 _attackArea)
    {
        Collider2D[] objectsToHit = Physics2D.OverlapBoxAll(
            _attackTransform.position, 
            _attackArea,
            0,
            attackableLayer
            );
        if (objectsToHit.Length > 0) Debug.Log("Hit");

        for (int i = 0; i < objectsToHit.Length; i++)
        {
            Collider2D curObj = objectsToHit[i];

            if (curObj.GetComponent<Enemy>() != null)
            {
                Vector2 hitDirection = (transform.position - curObj.transform.position).normalized;
                curObj.GetComponent<Enemy>().EnemyHit(damage, hitDirection, hitForce);
            }
        }
    }

    void SlashEffectAtAngle(GameObject _slashEffect, int _effectAngle, Transform _attackTransform)
    {
        _slashEffect = Instantiate(_slashEffect, _attackTransform);
        _slashEffect.transform.eulerAngles = new Vector3(0, 0, _effectAngle);
        _slashEffect.transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(sideAttackTransform.position, sideAttackArea);
        Gizmos.DrawWireCube(upAttackTransform.position, upAttackArea);
        Gizmos.DrawWireCube(downAttackTransform.position, downAttackArea);
    }
}