using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public GameObject hitEffect;

    public int bulletDamage = 50; 

   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        SecondEnemy secondEnemy = collision.GetComponent<SecondEnemy>();


        if (enemy != null)
        {
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, 0.3f);
            enemy.TakeDamage(bulletDamage);
            Destroy(gameObject);
        }

        if (secondEnemy != null)
        {
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, 0.3f);
            secondEnemy.TakeDamage(bulletDamage);
            Destroy(gameObject);
        }


        if (collision.gameObject.tag == "bulletCol")
        {
            Destroy(gameObject);
            
        }
    }
}
