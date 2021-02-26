using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class DeadScore : MonoBehaviour
{
    public Text deadScore;

    public static int deadScoreAmount;
    // Start is called before the first frame update
    void Start()
    {
        deadScore = GetComponent<Text>();
       
    }

    // Update is called once per frame
    void Update()
    {
        deadScoreAmount = Score.scoreAmount;

        deadScore.text = deadScoreAmount.ToString();
    }
}
