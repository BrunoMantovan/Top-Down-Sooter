using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public int explosionDamage = 100;
    public int explosionToPlayerDamage = 20;
    // Start is called before the first frame update
    void Start()
    {
        Handheld.Vibrate();
        Invoke("EndExplosion", 0.71f);
        FindObjectOfType<AudioManager>().Play("explosion1");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        SecondEnemy secondEnemy = collision.GetComponent<SecondEnemy>();
        PlayerController player = collision.GetComponent<PlayerController>();

        if(enemy != null)
        {
            enemy.TakeDamage(explosionDamage);
        }

        if(secondEnemy != null)
        {
            secondEnemy.TakeDamage(explosionDamage);
        }

        if(player != null)
        {
            player.TakeDamage(explosionToPlayerDamage);
        }
    }

    void EndExplosion()
    {
        Destroy(gameObject);
    }
}
