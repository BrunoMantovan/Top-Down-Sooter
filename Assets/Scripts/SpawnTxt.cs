using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnTxt : MonoBehaviour
{
    public Canvas startMessage;

    private void Awake()
    {
        startMessage.enabled = false;
    }

    void Start()
    {
        Invoke("StartTxt", 0.70f);

        Invoke("EndTxt", 7.70f);
    }

    void StartTxt()
    {
        startMessage.enabled = true;
    }

    void EndTxt()
    {
        gameObject.SetActive(false);
    }
}
