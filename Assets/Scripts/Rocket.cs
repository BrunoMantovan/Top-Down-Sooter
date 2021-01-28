using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public GameObject explosion;
    public Transform rocketTip;

    public int rocketDamage = 10;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("EndRcoket", 0.7f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy !=null)
        {
            enemy.TakeDamage(rocketDamage);
            EndRcoket();
        }
    }

    void EndRcoket()
    {
        GameObject effect = Instantiate(explosion, rocketTip.position, rocketTip.rotation);
        Destroy(effect, 0.71f);
        Destroy(gameObject);
    }
}
