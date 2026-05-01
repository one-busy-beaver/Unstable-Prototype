using UnityEngine;

public class RecoilHandler : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isRecoiling;
    private float recoilTimer;

    private void Awake() => rb = GetComponent<Rigidbody2D>();

    private void Update()
    {
        if (isRecoiling)
        {
            recoilTimer -= Time.deltaTime;
            if (recoilTimer <= 0)
            {
                isRecoiling = false;
            }
        }
    }

    public void ApplyRecoil(Vector2 direction, float force, float duration)
    {
        isRecoiling = true;
        recoilTimer = duration;

        rb.linearVelocity = Vector2.zero; // Reset current velocity
        rb.AddForce(direction * force, ForceMode2D.Impulse);
    }

    public bool IsRecoiling => isRecoiling;
}