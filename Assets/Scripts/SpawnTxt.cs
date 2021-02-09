using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnTxt : MonoBehaviour
{
    public Canvas startMessage;
    public float startTime;
    public float endTime;

    private void Awake()
    {
        startMessage.enabled = false;
    }

    void Start()
    {
        Invoke("StartTxt", startTime);

        Invoke("EndTxt", endTime);
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
