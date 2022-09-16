using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ExtraSpeedBuy : MonoBehaviour
{
  
    public int speedPrice;
    public PlayerController playerCont;
    private bool buyable;
    public TextMeshProUGUI speedCost;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Score.scoreAmount >= speedPrice && buyable == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                BuySpeed();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController playerCont = collision.GetComponent<PlayerController>();

        if (playerCont != null)
        {
            buyable = true;
            speedCost.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerController playerCont = collision.GetComponent<PlayerController>();

        if (playerCont != null)
        {
            buyable = false;
            speedCost.enabled = false;
        }
    }

    private void BuySpeed()
    {
        Score.scoreAmount -= speedPrice;
        this.gameObject.SetActive(false);
        playerCont.ExtraSpeed();
        FindObjectOfType<AudioManager>().Play("Coins");
    }
}
