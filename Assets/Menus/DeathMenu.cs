using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
   public void Retry()
    {
        SceneManager.LoadScene(1);
        Score.scoreAmount = 0;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }
}
