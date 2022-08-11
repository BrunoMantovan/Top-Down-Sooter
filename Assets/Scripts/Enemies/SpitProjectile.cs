using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitProjectile : MonoBehaviour
{
    public int projectileDamage = 10;

    public GameObject acidImpact;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();

        if(player != null)
        {
            GameObject impact = Instantiate(acidImpact, transform.position, Quaternion.identity);
            Destroy(impact, 0.61f);
            player.lifesDecrease();
            Destroy(gameObject);
        }

        if(collision.gameObject.tag == "bulletColl")
        {
            Destroy(gameObject);
        }
    }
}
