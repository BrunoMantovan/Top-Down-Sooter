using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public GameObject hitEffect;

    public int damage = 50; 

   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if(enemy != null)
        {
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, 0.3f);
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
        

        if(collision.gameObject.tag == "bulletCol")
        {
            Destroy(gameObject);
            
        }
    }
}
