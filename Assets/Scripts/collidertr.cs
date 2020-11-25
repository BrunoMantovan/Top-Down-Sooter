using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collidertr : MonoBehaviour
{
    public GameObject spawner;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        PlayerController player = collider.GetComponent<PlayerController>();
        

        if(player != null)
        {
            spawner.GetComponent<Spawner>().SpawnStart();
        }

    }
}
