using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public Transform explosionN1;
    public Transform explosionN2;
    public Transform explosionN3;
    public Transform explosionN4;

    public Animator anim;

    const string stateOnPlate = "IsOnPlate";

    public GameObject explosionPrefab;
    public GameObject explosionPrefab2;
    public float activationTimer;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        anim.SetBool(stateOnPlate, false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();

        if(player != null)
        {
            anim.SetBool(stateOnPlate, true);

            Invoke("startExplosions", activationTimer);
        }
    }

    public void startExplosions()
    {
        GameObject explosion1 = Instantiate(explosionPrefab, explosionN1.position, explosionN1.rotation);
        GameObject explosion2 = Instantiate(explosionPrefab, explosionN2.position, explosionN2.rotation);
        GameObject explosion3 = Instantiate(explosionPrefab, explosionN3.position, explosionN3.rotation);
        GameObject explosion4 = Instantiate(explosionPrefab, explosionN4.position, explosionN4.rotation);
        GameObject explosion5 = Instantiate(explosionPrefab2, transform.position, Quaternion.identity);

        endPlate();
    }

    public void endPlate()
    {
        Destroy(gameObject);
    }
}
