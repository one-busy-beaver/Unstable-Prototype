using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] float speed = 15f;
    [SerializeField] int damage = 3;
    [SerializeField] float lifetime = 1f;

    Rigidbody2D rb;

    void Awake() => rb = GetComponent<Rigidbody2D>();

    void Start() => Destroy(gameObject, lifetime); // Self-destruct after a few seconds

    public void Setup(float direction)
    {
        // Set horizontal velocity and flip the sprite if needed
        rb.linearVelocity = new Vector2(direction * speed, 0);
        rb.gravityScale = 0f;

        transform.localScale = new Vector3(direction, 1, 1);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Reuse your enemy hit logic
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.EnemyHit(damage, transform.position);
            Destroy(gameObject); // Destroy on hit
        }
        
        // Destroy if it hits a wall
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Destroy(gameObject);
        }
    }
}