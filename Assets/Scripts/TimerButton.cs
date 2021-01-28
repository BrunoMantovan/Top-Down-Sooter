using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerButton : MonoBehaviour
{
    public GameObject timerCircle;
    public float timer = 10f;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("TimerCircleOff", timer);
    }

    void TimerCircleOff()
    {
        timerCircle.SetActive(false);
    }
}
