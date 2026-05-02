using UnityEngine;

public class PlayerCast : MonoBehaviour
{
    [Header("Casting Settings")]
    [SerializeField] GameObject fireballPrefab;
    [SerializeField] Transform launchPoint; // Create an empty child object where the fireball starts
    [SerializeField] float castCooldown = 0.8f;

    Animator anim;
    float cooldownTimer;

    void Start() => anim = GetComponent<Animator>();

    void Update()
    {
        cooldownTimer += Time.deltaTime;

        // Use whatever input key you prefer
        if (InputManager.Instance.Controls.Player.Cast.triggered && cooldownTimer >= castCooldown)
        {
            Cast();
        }
    }

    void Cast()
    {
        cooldownTimer = 0;
        anim.SetTrigger("Casting");

        // We instantiate the fireball at the launch point
        GameObject ball = Instantiate(fireballPrefab, launchPoint.position, Quaternion.identity);
        
        // Pass the player's current facing direction (1 or -1) to the fireball
        float direction = transform.localScale.x;
        ball.GetComponent<Fireball>().Setup(direction);
    }
}