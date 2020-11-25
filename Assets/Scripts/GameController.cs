using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private int deadEnemies = 0;

    public OpenDorr OP;
    

    public void EnemyHasDied()
    {
        deadEnemies += 1;

        if(deadEnemies == 10)
        {
            OP.OpenDoor();

            Debug.Log("hola");
        }
    }

   
}
