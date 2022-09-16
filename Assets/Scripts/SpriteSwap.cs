using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpriteSwap : MonoBehaviour
{
    public Shooting shootingScript;
    private Image image;
    public PlayerController playerCont;
    private Sprite LaserMK1, LaserMK2, PlasmaCannon, Shotgun;
    private string currentWeaponName;
    public TextMeshProUGUI weaponName;


    private void Start()
    {
        image = GetComponent<Image>();
        LaserMK1 = Resources.Load<Sprite>("LMK1");
        LaserMK2 = Resources.Load<Sprite>("LMK2");
        PlasmaCannon = Resources.Load<Sprite>("Cannon");
        Shotgun = Resources.Load<Sprite>("Shotgun");
       
    }

    private void Update()
    {
        weaponName.text = currentWeaponName;

        if (playerCont.bulletBool == true && playerCont.rotationOn == true)
        {
            image.sprite = LaserMK1;
            shootingScript.LMK1.SetActive(true);
            shootingScript.LMK2.SetActive(false);
            shootingScript.Cannon.SetActive(false);
            shootingScript.Shotgun.SetActive(false);
            weaponName.text = "LMK1";
        }
        else if (playerCont.bullet2Bool == true && playerCont.rotationOn == true)
        {
            image.sprite = LaserMK2;
            shootingScript.LMK2.SetActive(true);
            shootingScript.LMK1.SetActive(false);
            shootingScript.Cannon.SetActive(false);
            shootingScript.Shotgun.SetActive(false);
            currentWeaponName = "LMK2";
        }
        else if (playerCont.rocketBool == true && playerCont.rotationOn == true)
        {
            image.sprite = PlasmaCannon;
            shootingScript.Cannon.SetActive(true);
            shootingScript.LMK1.SetActive(false);
            shootingScript.LMK2.SetActive(false);
            shootingScript.Shotgun.SetActive(false);
            currentWeaponName = "Plasma Cannon";
        }
        else if (playerCont.shotgunBool == true && playerCont.rotationOn == true)
        {
            image.sprite = Shotgun;
            shootingScript.Shotgun.SetActive(true);
            shootingScript.LMK1.SetActive(false);
            shootingScript.LMK2.SetActive(false);
            shootingScript.Cannon.SetActive(false);
            currentWeaponName = "Shotgun";      
        }
    }
}
