using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collidertr : MonoBehaviour
{
    
    public float spawnDelay = 4f;

   void Start()
    {
        Invoke("SpawnStart", spawnDelay);
    }

    public void SpawnStart()
    {
        GetComponent<Spawner>().enabled = true;
    }
}
