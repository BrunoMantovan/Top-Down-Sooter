using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class melee : MonoBehaviour
{
    public int meleeDamage = 100;

    private void Start()
    {
        Invoke("FinishMelee", 0.5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        SecondEnemy secondEnemy = collision.GetComponent<SecondEnemy>();
        ThirdEnemy thirdEnemy = collision.GetComponent<ThirdEnemy>();

        if(enemy != null)
        {
            enemy.TakeDamage(meleeDamage);
        }

        if(secondEnemy != null)
        {
            secondEnemy.TakeDamage(meleeDamage);
        }

        if(thirdEnemy != null)
        {
            thirdEnemy.TakeDamage(meleeDamage);
        }
    }

    public void FinishMelee()
    {
        gameObject.SetActive(false);
    }
}
