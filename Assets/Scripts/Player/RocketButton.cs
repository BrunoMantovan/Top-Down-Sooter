using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RocketButton : MonoBehaviour
{
    public Button RktButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void RocketButtonOn()
    {
        RktButton.interactable = true;
    }
}
