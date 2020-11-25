using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderEnemy : MonoBehaviour
{
    public GameObject enemy;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        PlayerController player = collider.GetComponent<PlayerController>();


        if (player != null)
        {
            enemy.GetComponent<Enemy>().EnemyStart();
        }

    }
}
