using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    public GameObject alien;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("SpawnAlien", 1f);
    }


    void SpawnAlien()
    {
        Instantiate(alien, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
}
