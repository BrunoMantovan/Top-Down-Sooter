using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState
{
    inMenu,
    inGame,
    dead
}

public class GameManager : MonoBehaviour
{

    public GameState currentGameState = GameState.inGame;

    public static GameManager sharedInstance;

    private PlayerController playerCont;

    public int killedEnemies = 0;

    public Canvas deadCanvas;
    public Canvas joystickCanvas;


    private void Awake()
    {
        if(sharedInstance == null)
        {
            sharedInstance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerCont = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentGameState == GameState.inGame)
        {
            Time.timeScale = 1;
        }
        else if(currentGameState == GameState.inMenu || currentGameState == GameState.dead)
        {
            Time.timeScale = 0;
        }


    }

    public void StartGame()
    {
        SetGameState(GameState.inGame);
               
    }

    public void PauseGame()
    {
        SetGameState(GameState.inMenu);
        
    }

    public void DeathTrigger()
    {
        SetGameState(GameState.dead);

        deadCanvas.enabled = true;
        joystickCanvas.enabled = false;
    }

    private void SetGameState(GameState newGameState)
    {
        if(newGameState == GameState.inMenu)
        {

        }
        else if(newGameState == GameState.inGame)
        {

        }

        this.currentGameState = newGameState;
    }

   
}
