using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private int deadEnemies = 0;

    public OpenDorr OP;

    public int totalDeadEnemies = 10;
    

    public void EnemyHasDied()
    {
        deadEnemies += 1;

        if(deadEnemies == totalDeadEnemies)
        {
            OP.OpenDoor();

            Debug.Log("completado");
        }
    }

   
}
