using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ExtraDamage : MonoBehaviour
{
    public int damagePrice;
    public PlayerController playerCont;
    private bool buyable;
    public TextMeshProUGUI damageCost;
    public Shooting shootingScript;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Score.scoreAmount >= damagePrice && buyable == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                BuyDamage();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController playerCont = collision.GetComponent<PlayerController>();

        if (playerCont != null)
        {
            buyable = true;
            damageCost.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerController playerCont = collision.GetComponent<PlayerController>();

        if (playerCont != null)
        {
            buyable = false;
            damageCost.enabled = false;
        }
    }

    private void BuyDamage()
    {
        Score.scoreAmount -= damagePrice;
        this.gameObject.SetActive(false);
        shootingScript.newBulletDamage();
        FindObjectOfType<AudioManager>().Play("Coins");
    }
}
