using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    Text scoreText;
    public static int scoreAmount;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<Text>();

        PlayerPrefs.GetInt("score");
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = scoreAmount.ToString();
        PlayerPrefs.SetInt("score", scoreAmount);
    }
}
