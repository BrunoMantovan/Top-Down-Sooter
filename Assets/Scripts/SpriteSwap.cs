using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteSwap : MonoBehaviour
{

    private Image image;

    private Sprite LaserMK1, LaserMK2;

    private void Start()
    {
        image = GetComponent<Image>();
        LaserMK1 = Resources.Load<Sprite>("LaserMK1");
        LaserMK2 = Resources.Load<Sprite>("LaserMK2");

        image.sprite = LaserMK1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (image.sprite == LaserMK1)
            {
                image.sprite = LaserMK2;
            }
            else if(image.sprite == LaserMK2)
            {
                image.sprite = LaserMK1;
            }
        }
    }
}
