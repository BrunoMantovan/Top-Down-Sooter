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
        ThirdEnemy thirdEnemy = collision.GetComponent<ThirdEnemy>();


        if (enemy != null)
        {
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, 0.4f);
            enemy.TakeDamage(bulletDamage);
            Destroy(gameObject);
        }

        if (secondEnemy != null)
        {
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, 0.4f);
            secondEnemy.TakeDamage(bulletDamage);
            Destroy(gameObject);
        }

        if(thirdEnemy != null)
        {
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, 0.4f);
            thirdEnemy.TakeDamage(bulletDamage);
            Destroy(gameObject);
        }


        if (collision.gameObject.tag == "bulletCol")
        {
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, 0.4f);
            
            Destroy(gameObject);
        }
    }
}
