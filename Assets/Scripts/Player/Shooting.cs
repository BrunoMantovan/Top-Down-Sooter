using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Shooting : MonoBehaviour
{
    public Transform fireTip;
    public Transform LMK2FireTip;
    public Transform CannonFireTip;
    public Transform ShotgunFireTip;
    public GameObject LMK1;
    public GameObject LMK2;
    public GameObject Cannon;
    public GameObject Shotgun;

    public GameObject bulletPrefab;
    public GameObject bulletPrefab2;
    public GameObject rocketPrefab;
    public GameObject pelletPrefab;
    public GameObject newBulletPrefab;
    public int pelletAmount;

    public float spread;
    public float bulletForce = 20f;
    public float bullet2Force = 15f;
    public float rocketForce = 20f;
    public float pelletForce = 60f;

    public float fireRate = 1f;
    public float bullet2FireRate = 1f;
    public float rocketFireRate = 1f;
    public float shotgunFireRate = 0.75f;
    float nextFire = 0f;

    public int maxAmmoLMK2 = 100;
    public int currentAmmoLMK2;
    public int maxAmmoPlasmaCannon = 3;
    public int currentAmmoPlasmaCannon;
    public int maxAmmoShotgun = 20;
    public int currentAmmoShotgun;

    public bool LMK2Active;
    public bool PlasmaCannonActive;
    public bool ShotgunActive;

    public PlayerController playerCont;

    List<Quaternion> pellets;

    public TextMeshProUGUI InfiniteAmmo;
    public TextMeshProUGUI LMK2AmmoDisplay;
    public TextMeshProUGUI PlasmaCannonAmmoDisplay;
    public TextMeshProUGUI ShotgunAmmoDisplay;

    public int extraBulletDamage = 0;

    private void Awake()
    {
        pellets = new List<Quaternion>(pelletAmount);
        for (int i = 0; i < pelletAmount; i++)
        {
            pellets.Add(Quaternion.Euler(Vector3.zero));
        }
    }
    private void Start()
    {
        currentAmmoLMK2 = PlayerPrefs.GetInt("LMK2Ammo", currentAmmoLMK2);
        currentAmmoPlasmaCannon = PlayerPrefs.GetInt("cannonAmmo", currentAmmoPlasmaCannon);
        currentAmmoShotgun = PlayerPrefs.GetInt("shotgunAmmo", currentAmmoShotgun);

        extraBulletDamage = PlayerPrefs.GetInt("ExtraBulletDamage", extraBulletDamage);
    }
    void Update()
    {
        LMK2AmmoDisplay.text = currentAmmoLMK2.ToString();
        PlasmaCannonAmmoDisplay.text = currentAmmoPlasmaCannon.ToString();
        ShotgunAmmoDisplay.text = currentAmmoShotgun.ToString();

        if (currentAmmoLMK2 <= 0 && LMK2Active == true)
        {
            LMK2Active = false;
            playerCont.False();
            return;
        }

        if (currentAmmoPlasmaCannon <= 0 && PlasmaCannonActive == true)
        {
            PlasmaCannonActive = false;
            playerCont.False2();
            return;
        }

        if(currentAmmoShotgun <= 0 && ShotgunActive == true)
        {
            ShotgunActive = false;
            playerCont.False3();
            return;
        }

        if (playerCont.bullet2Shoot == true)
        {
            PlasmaCannonAmmoDisplay.enabled = false;
            LMK2AmmoDisplay.enabled = true;
            InfiniteAmmo.enabled = false;
            ShotgunAmmoDisplay.enabled = false;
        }
        else if (playerCont.rocketAble == true)
        {
            LMK2AmmoDisplay.enabled = false;
            PlasmaCannonAmmoDisplay.enabled = true;
            InfiniteAmmo.enabled = false;
            ShotgunAmmoDisplay.enabled = false;
        }
        else if(playerCont.shotgunAble == true)
        {
            ShotgunAmmoDisplay.enabled = true;
            LMK2AmmoDisplay.enabled = false;
            PlasmaCannonAmmoDisplay.enabled = false;
            InfiniteAmmo.enabled = false;
        }
        else
        {
            ShotgunAmmoDisplay.enabled = false;
            LMK2AmmoDisplay.enabled = false;
            PlasmaCannonAmmoDisplay.enabled = false;
            InfiniteAmmo.enabled = true;
        }
        

        //NormalBullet
        if((playerCont.bulletBool == true) && (playerCont.ableToShoot == true))
        {
            if (Input.GetKey(KeyCode.Mouse0) && Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;

                FindObjectOfType<AudioManager>().Play("shoot");

                Shoot();
            }
        }

        //Bullet2
        if (playerCont.bullet2Shoot == true && playerCont.ableToShoot == true)
        {
            if (Input.GetKey(KeyCode.Mouse0) && Time.time > nextFire)
            {
                nextFire = Time.time + bullet2FireRate;

                FindObjectOfType<AudioManager>().Play("shoot2");

                Shoot2();
            }
        }

        //Rocket
        if(playerCont.rocketAble == true && playerCont.ableToShoot == true)
        {
            if (Input.GetKey(KeyCode.Mouse0) && Time.time > nextFire)
            {
                nextFire = Time.time + rocketFireRate;

                FindObjectOfType<AudioManager>().Play("Plasma Rocket");

                //Shoot3();
                Invoke("Shoot3", 0.5f);
            }
        }

        //Shotgun
        if(playerCont.shotgunAble == true && playerCont.ableToShoot == true)
        {
            if (Input.GetKey(KeyCode.Mouse0) && Time.time > nextFire)
            {
                nextFire = Time.time + shotgunFireRate;

                FindObjectOfType<AudioManager>().Play("Shotgun-Fire");

                Shoot4();
            }
        }
    }
    
    //LMK1
    void Shoot()
    {
        if(extraBulletDamage == 0)
        {
            GameObject Bullet = Instantiate(bulletPrefab, fireTip.position, fireTip.rotation);
            Rigidbody2D rb = Bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(fireTip.up * bulletForce, ForceMode2D.Impulse);
        }
        else if (extraBulletDamage == 1)
        {
            GameObject newBullet = Instantiate(newBulletPrefab, fireTip.position, fireTip.rotation);
            Rigidbody2D rb = newBullet.GetComponent<Rigidbody2D>();
            rb.AddForce(fireTip.up * bulletForce, ForceMode2D.Impulse);
        }
    }

    //LMK2
    void Shoot2()
    {
        currentAmmoLMK2--;
        PlayerPrefs.SetInt("LMK2Ammo", currentAmmoLMK2);
        GameObject Bullet2 = Instantiate(bulletPrefab2, LMK2FireTip.position, LMK2FireTip.rotation);
        Rigidbody2D rb2 = Bullet2.GetComponent<Rigidbody2D>();
        rb2.AddForce(LMK2FireTip.up * bullet2Force, ForceMode2D.Impulse);
    }

    //Cannon
    void Shoot3()
    {
        currentAmmoPlasmaCannon--;
        PlayerPrefs.SetInt("cannonAmmo", currentAmmoPlasmaCannon);
        GameObject Rocket = Instantiate(rocketPrefab, CannonFireTip.position, CannonFireTip.rotation);
        Rigidbody2D rb3 = Rocket.GetComponent<Rigidbody2D>();
        rb3.AddForce(CannonFireTip.up * rocketForce, ForceMode2D.Impulse);
    }

    //Shotgun
    void Shoot4()
    {
        currentAmmoShotgun = currentAmmoShotgun -4;
        PlayerPrefs.SetInt("shotgunAmmo", currentAmmoShotgun);
        for (int i = 0; i < pelletAmount; i++)
        {
            pellets[i] = Random.rotation;
            GameObject p = Instantiate(pelletPrefab, ShotgunFireTip.position, ShotgunFireTip.rotation);
            p.transform.rotation = Quaternion.RotateTowards(p.transform.rotation, pellets[i], spread);
            p.GetComponent<Rigidbody2D>().AddForce(p.transform.up * pelletForce);
            i++;
        }
    }

    public void SetMaxLMK2()
    {
        currentAmmoLMK2 = maxAmmoLMK2;
        PlayerPrefs.SetInt("LMK2Ammo", currentAmmoLMK2);
        LMK2Active = true;
    }
    public void SetMaxCannon()
    {
        currentAmmoPlasmaCannon = maxAmmoPlasmaCannon;
        PlayerPrefs.SetInt("cannonAmmo", currentAmmoPlasmaCannon);
        PlasmaCannonActive = true;
    }

    public void SetMaxShotgun()
    {
        currentAmmoShotgun = maxAmmoShotgun;
        PlayerPrefs.SetInt("shotgunAmmo", currentAmmoShotgun);
        ShotgunActive = true;
    }

    public void newBulletDamage()
    {
        extraBulletDamage = 1;
        PlayerPrefs.SetInt("ExtraBulletDamage", extraBulletDamage);
    }
}
