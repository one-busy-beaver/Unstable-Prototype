using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health = 6;
    [SerializeField] float recoilLength = 1;
    [SerializeField] float recoilFactor = 50;
    [SerializeField] bool isRecoiling = false;

    float recoilTimer;
    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        TakeRecoil();
        HanldeDeath();
    }

    void TakeRecoil()
    {
        if (isRecoiling)
        {
            if (recoilTimer < recoilLength)
            {
                recoilTimer += Time.deltaTime;
            }
            else
            {
                isRecoiling = false;
                recoilTimer = 0;
            }
        }
    }

    void HanldeDeath()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void EnemyHit(float _damageDone, Vector2 _hitDirection, float _hitForce)
    {
        health -= _damageDone;
        if(!isRecoiling)
        {
            rb.AddForce(-_hitForce * recoilFactor * _hitDirection);
        }
    }
}