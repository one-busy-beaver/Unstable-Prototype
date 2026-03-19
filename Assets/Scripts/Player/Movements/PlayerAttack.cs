using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attacking Settings")]
    //[SerializeField] private Transform sideAttackTransform;
    //[SerializeField] private Transform upAttackTransform;
    //[SerializeField] private Transform downAttackTransform;
    [SerializeField] private Vector2 sideAttackArea = new Vector2(4.5f, 3.5f); 
    [SerializeField] private Vector2 upAttackArea = new Vector2(3.5f, 4.5f);
    [SerializeField] private Vector2 downAttackArea = new Vector2(3.5f, 4.5f);
    private bool attack = false;
    private float timeBetweenAttack;
    private float timeSinceAttack;

    /*void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(sideAttackTransform.position, sideAttackArea);
        Gizmos.DrawWireCube(upAttackTransform.position, upAttackArea);
        Gizmos.DrawWireCube(downAttackTransform.position, downAttackArea);
    }*/

    void Update()
    {
        // attack = InputManager.Instance.Controls.Player.Attack.IsPressed();
        Attack();
    }

    void Attack()
    {
        timeSinceAttack += Time.deltaTime;

        if (attack && timeSinceAttack >= timeBetweenAttack) {
            timeSinceAttack = 0;
        }
    } 
}