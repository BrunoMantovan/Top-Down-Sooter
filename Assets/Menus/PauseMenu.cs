using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameManager gameMang;
    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && gameMang.currentGameState == GameState.inMenu)
        {
            gameObject.SetActive(false);
            gameMang.StartGame();
        }
    }
}
