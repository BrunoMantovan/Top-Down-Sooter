using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateExplosion : MonoBehaviour
{
    public int explosionDamage = 50;
    
    // Start is called before the first frame update
    void Start()
    {
        Handheld.Vibrate();
        Invoke("EndExplosion", 0.77f);
        FindObjectOfType<AudioManager>().Play("explosion2");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        SecondEnemy secondEnemy = collision.GetComponent<SecondEnemy>();
        PlayerController player = collision.GetComponent<PlayerController>();

        if (enemy != null)
        {
            enemy.TakeDamage(explosionDamage);
        }

        if (secondEnemy != null)
        {
            secondEnemy.TakeDamage(explosionDamage);
        }

        if (player != null)
        {
            player.explosionHit();
        }
    }

    private void OnTriggerExit2D(Collider2D exitCollision)
    {
        PlayerController player = exitCollision.GetComponent<PlayerController>();

        if(player != null)
        {
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }


    void EndExplosion()
    {
        Destroy(gameObject);
    }
}
