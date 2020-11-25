using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform fireTip;
    public GameObject bulletPrefab;

    public float bulletForce = 20f; //Velocidad de la bala

    public float fireRate = 1f;
    float nextFire = 0f;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Shoot();
        }
    }
    
    void Shoot()
    {
        GameObject Bullet = Instantiate(bulletPrefab, fireTip.position, fireTip.rotation);
        Rigidbody2D rb = Bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(fireTip.up * bulletForce, ForceMode2D.Impulse);
    }
}
