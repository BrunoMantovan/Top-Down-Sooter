using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemy;
    public Transform[] spawnSpots;
    private float timeBtwnSpawns;
    public float startTimeBtwnSpawns;

    public int maxEnemies = 10;
    int enemyCounter = 0; 

    void Awake()
    {
        GetComponent<Spawner>().enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        timeBtwnSpawns = startTimeBtwnSpawns;       
    }

    // Update is called once per frame
    void Update()
    {
        if(timeBtwnSpawns <= 0 && enemyCounter < maxEnemies)
        {
            int randPos = Random.Range(0, spawnSpots.Length - 1);
            Instantiate(enemy, spawnSpots[randPos].position, Quaternion.identity);
            timeBtwnSpawns = startTimeBtwnSpawns;
            enemyCounter++;
        }
        else
        {
            timeBtwnSpawns -= Time.deltaTime;
        }        
    }
    
   
        
   
}
