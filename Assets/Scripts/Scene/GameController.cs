using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private int deadEnemies = 0;

    public OpenDorr OP;
    public OpenDorr OP2;
    

    public GameObject leaveMsg;

    public int totalDeadEnemies = 10;
    

    public void EnemyHasDied()
    {
        deadEnemies += 1;

        if(deadEnemies == totalDeadEnemies)
        {
            OP.OpenDoor();
            OP2.OpenDoor();
            

            leaveMsg.SetActive(true);

            Debug.Log("completado");
        }
    }

   
}
