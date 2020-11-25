﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState
{
    inMenu,
    inGame
}

public class GameManager : MonoBehaviour
{

    public GameState currentGameState = GameState.inMenu;

    public static GameManager sharedInstance;

    private PlayerController playerCont;

    public int killedEnemies = 0;

    

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
        else if(currentGameState == GameState.inMenu)
        {
            Time.timeScale = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseGame();
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
