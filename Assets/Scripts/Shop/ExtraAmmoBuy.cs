using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ExtraAmmoBuy : MonoBehaviour
{
    public int extraAmmoPrice;
    public PlayerController playerCont;
    private bool buyable;
    public TextMeshProUGUI extraAmmoCost;
    public Shooting shootingScript;

    // Update is called once per frame
    void Update()
    {
        if (Score.scoreAmount >= extraAmmoPrice && buyable == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                BuyExtraAmmo();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController playerCont = collision.GetComponent<PlayerController>();

        if (playerCont != null)
        {
            buyable = true;
            extraAmmoCost.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerController playerCont = collision.GetComponent<PlayerController>();

        if (playerCont != null)
        {
            buyable = false;
            extraAmmoCost.enabled = false;
        }
    }

    private void BuyExtraAmmo()
    {
        Score.scoreAmount -= extraAmmoPrice;
        this.gameObject.SetActive(false);
        shootingScript.NewMaxAmmo();
        FindObjectOfType<AudioManager>().Play("Coins");
    }
}
