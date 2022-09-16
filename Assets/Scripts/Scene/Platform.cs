using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Platform : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Sprite platformGreen, platformNormal;
    public int turretPrice;
    public bool buyable;
    public GameObject turret;
    public TextMeshProUGUI turretText;
    
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();

        platformGreen = Resources.Load<Sprite>("platformGreen");
        platformNormal = Resources.Load<Sprite>("platformNormal");
    }

    private void Update()
    {
        if (Score.scoreAmount >= turretPrice && buyable == true)
        {
            if (Input.GetKey(KeyCode.E))
            {

                BuyTurret();
                
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController playerCont = collision.GetComponent<PlayerController>();

        if(playerCont != null)
        {
            buyable = true;
            turretText.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerController playerCont = collision.GetComponent<PlayerController>();
        if (playerCont != null)
        {
            buyable = false;
            turretText.enabled = false;
        }
    }

    private void BuyTurret()
    {
        Score.scoreAmount -= turretPrice;
        turret.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
