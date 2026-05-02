using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health = 6;
    

    void Update()
    {
        HanldeDeath();
    }

    void HanldeDeath()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void EnemyHit(float _damageDone)
    {
        health -= _damageDone;
        if (health <= 0)
            Destroy(gameObject);
    }
}