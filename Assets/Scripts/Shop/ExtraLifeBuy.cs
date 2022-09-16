using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ExtraLifeBuy : MonoBehaviour
{
    public int lifePrice;
    public PlayerController playerCont;
    private bool buyable;
    public TextMeshProUGUI lifeCost;
   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if(Score.scoreAmount >= lifePrice && buyable == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                BuyExtraLife();
            }
        } 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController playerCont = collision.GetComponent<PlayerController>();

        if (playerCont != null)
        {
            buyable = true;
            lifeCost.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerController playerCont = collision.GetComponent<PlayerController>();

        if (playerCont != null)
        {
            buyable = false;
            lifeCost.enabled = false;
        }
    }

    private void BuyExtraLife()
    {
        Score.scoreAmount -= lifePrice;
        this.gameObject.SetActive(false);
        playerCont.LifeIncrease();
        FindObjectOfType<AudioManager>().Play("Coins");
    }
}
