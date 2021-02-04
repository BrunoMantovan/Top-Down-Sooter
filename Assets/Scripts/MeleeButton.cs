using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeleeButton : MonoBehaviour
{
    public Button meleeButton;

    public int timer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Timer()
    {
        Invoke("MeleeButtonOn", timer);
    }

    public void MeleeButtonOn()
    {
        meleeButton.interactable = true;
    }


}
