using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAnimStarter : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<PlayerController>().Spawn();

    }
}
