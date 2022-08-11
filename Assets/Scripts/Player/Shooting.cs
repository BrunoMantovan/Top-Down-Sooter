using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform fireTip;
    public GameObject bulletPrefab;
    public GameObject bulletPrefab2;
    public GameObject rocketPrefab;

    public float bulletForce = 20f;
    public float bullet2Force = 15f;
    public float rocketForce = 20f;

    public float fireRate = 1f;
    public float bullet2FireRate = 1f;
    public float rocketFireRate = 1f;
    float nextFire = 0f;

    public Joystick shootJoystick;

    public PlayerController playerCont;


    // Update is called once per frame
    void Update()
    {
        //NormalBullet
        if((playerCont.bullet2Bool == false) && (playerCont.rocketBool == false) && (playerCont.ableToShoot == true))
        {
            if ((shootJoystick.Horizontal >= .2f || shootJoystick.Horizontal <= -.2f || shootJoystick.Vertical >= .2f || shootJoystick.Vertical <= -.2f || Input.GetKey(KeyCode.Mouse0)) && Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;

                FindObjectOfType<AudioManager>().Play("shoot");

                Shoot();
            }
        }

        //Bullet2
        if (playerCont.bullet2Bool == true && playerCont.ableToShoot == true)
        {
            if ((shootJoystick.Horizontal >= .2f || shootJoystick.Horizontal <= -.2f || shootJoystick.Vertical >= .2f || shootJoystick.Vertical <= -.2f || Input.GetKey(KeyCode.Mouse0)) && Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;

                FindObjectOfType<AudioManager>().Play("shoot2");

                Shoot2();
            }
        }

        //Rocket
        if(playerCont.rocketBool == true && playerCont.ableToShoot == true)
        {
            if ((shootJoystick.Horizontal >= .2f || shootJoystick.Horizontal <= -.2f || shootJoystick.Vertical >= .2f || shootJoystick.Vertical <= -.2f || Input.GetKey(KeyCode.Mouse0)) && Time.time > nextFire)
            {
                nextFire = Time.time + rocketFireRate;

                FindObjectOfType<AudioManager>().Play("Plasma Rocket");

                //Shoot3();
                Invoke("Shoot3", 0.5f);
            }
        }

    }
    
    void Shoot()
    {

        GameObject Bullet = Instantiate(bulletPrefab, fireTip.position, fireTip.rotation);
        Rigidbody2D rb = Bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(fireTip.up * bulletForce, ForceMode2D.Impulse);
    }

    void Shoot2()
    {
        GameObject Bullet2 = Instantiate(bulletPrefab2, fireTip.position, fireTip.rotation);
        Rigidbody2D rb2 = Bullet2.GetComponent<Rigidbody2D>();
        rb2.AddForce(fireTip.up * bulletForce, ForceMode2D.Impulse);
    }

    void Shoot3()
    {
        GameObject Rocket = Instantiate(rocketPrefab, fireTip.position, fireTip.rotation);
        Rigidbody2D rb3 = Rocket.GetComponent<Rigidbody2D>();
        rb3.AddForce(fireTip.up * rocketForce, ForceMode2D.Impulse);
    }
}
