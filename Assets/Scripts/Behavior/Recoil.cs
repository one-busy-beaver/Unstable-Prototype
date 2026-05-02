using UnityEngine;
using System.Collections;

public class Recoil : MonoBehaviour
{
    [SerializeField] float recoilTime = 0.15f;
    [SerializeField] float recoilSpeed = 5f;

    Rigidbody2D rb;
    public bool IsRecoiling { get; private set; }

    void Awake() => rb = GetComponent<Rigidbody2D>();

    public void TriggerRecoil(Vector2 hitSourcePosition, bool snap = true, float multiplier = 1f)
    {
        if (IsRecoiling) return;
        
        // Calculate direction from source to this object
        Vector2 rawDir = ((Vector2)transform.position - hitSourcePosition).normalized;
        Vector2 snappedDir = snap ? SnapToCardinal(rawDir) : rawDir;
        
        StartCoroutine(RecoilRoutine(snappedDir, multiplier));
    }

    Vector2 SnapToCardinal(Vector2 dir)
    {
        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
            return new Vector2(Mathf.Sign(dir.x), 0);
        else
            return new Vector2(0, Mathf.Sign(dir.y));
    }

    IEnumerator RecoilRoutine(Vector2 direction, float multiplier)
    {
        IsRecoiling = true;
        
        float timer = 0;
        while (timer < recoilTime)
        {
            timer += Time.deltaTime;
            // Set velocity directly for a consistent, short burst
            rb.linearVelocity = direction * recoilSpeed * multiplier; 
            yield return null;
        }

        rb.linearVelocity = Vector2.zero;
        IsRecoiling = false;
    }
}