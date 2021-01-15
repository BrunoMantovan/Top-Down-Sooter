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

    public Joystick shootJoystick;
    
    // Update is called once per frame
    void Update()
    {
        if ((shootJoystick.Horizontal >= .2f || shootJoystick.Horizontal <= -.2f || shootJoystick.Vertical >= .2f || shootJoystick.Vertical <= -.2f) && Time.time > nextFire)
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
