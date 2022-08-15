using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteSwap : MonoBehaviour
{
    public Shooting shootingScript;
    private Image image;
    public PlayerController playerCont;
    private Sprite LaserMK1, LaserMK2, PlasmaCannon;

    private void Start()
    {
        image = GetComponent<Image>();
        LaserMK1 = Resources.Load<Sprite>("LMK1");
        LaserMK2 = Resources.Load<Sprite>("LMK2");
        PlasmaCannon = Resources.Load<Sprite>("Cannon");
        //image.sprite = LaserMK1;
    }

    private void Update()
    {
        if (playerCont.bulletBool == true)
        {
            image.sprite = LaserMK1;
            shootingScript.LMK1.SetActive(true);
            shootingScript.LMK2.SetActive(false);
            shootingScript.Cannon.SetActive(false);
        }
        else if (playerCont.bullet2Bool == true)
        {
            image.sprite = LaserMK2;
            shootingScript.LMK2.SetActive(true);
            shootingScript.LMK1.SetActive(false);
            shootingScript.Cannon.SetActive(false);
        }
        else if (playerCont.rocketBool == true)
        {
            image.sprite = PlasmaCannon;
            shootingScript.Cannon.SetActive(true);
            shootingScript.LMK1.SetActive(false);
            shootingScript.LMK2.SetActive(false);
        }
    }
}
