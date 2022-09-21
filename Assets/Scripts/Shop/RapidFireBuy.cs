using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RapidFireBuy : MonoBehaviour
{
    public int rapidFirePrice;
    public PlayerController playerCont;
    private bool buyable;
    public TextMeshProUGUI rapidFireCost;
    public Shooting shootingScript;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Score.scoreAmount >= rapidFirePrice && buyable == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                BuyRapidFire();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController playerCont = collision.GetComponent<PlayerController>();

        if (playerCont != null)
        {
            buyable = true;
            rapidFireCost.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerController playerCont = collision.GetComponent<PlayerController>();

        if (playerCont != null)
        {
            buyable = false;
            rapidFireCost.enabled = false;
        }
    }

    private void BuyRapidFire()
    {
        Score.scoreAmount -= rapidFirePrice;
        this.gameObject.SetActive(false);
        shootingScript.NewFireRate();
        FindObjectOfType<AudioManager>().Play("Coins");
    }
}
