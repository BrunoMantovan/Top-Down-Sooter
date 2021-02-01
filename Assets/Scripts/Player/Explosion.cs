using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public int explosionDamage = 100;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("EndExplosion", 0.71f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        SecondEnemy secondEnemy = collision.GetComponent<SecondEnemy>();

        if(enemy != null)
        {
            enemy.TakeDamage(explosionDamage);
        }

        if(secondEnemy != null)
        {
            secondEnemy.TakeDamage(explosionDamage);
        }
    }

    void EndExplosion()
    {
        Destroy(gameObject);
    }
}
