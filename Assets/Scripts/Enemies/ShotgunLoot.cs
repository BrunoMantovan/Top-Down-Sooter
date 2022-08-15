using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunLoot : MonoBehaviour
{
    public PlayerController playerCont;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("EndTime", 15f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();

        if (player != null)
        {
            Destroy(gameObject);
        }
    }

    void EndTime()
    {
        Destroy(gameObject);
    }
}
