using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject powerupPrefab;
    public GameObject enemyPrefab;
    //enemy spawn position variables
    private float xMin = -9.5f;
    private float xMax = 9.5f;
    private float yPos = 5.5f;
    //Enemy variables
    public int waveNumber = 1;
    public int enemyCount;
    //powerup spawn locations
    public GameObject[] spawns;
    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(waveNumber);
        //Spawns powerup at the start of the game
        int index = UnityEngine.Random.Range(0, spawns.Length);
        Instantiate(powerupPrefab, spawns[index].transform.position, powerupPrefab.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<EnemyController>().Length;

        if (enemyCount == 0)
        {
            waveNumber++;
            SpawnEnemyWave(waveNumber);
            //New powerup every wave
            int index = UnityEngine.Random.Range(0, spawns.Length);
            Instantiate(powerupPrefab, spawns[index].transform.position, powerupPrefab.transform.rotation);
        }
    }
    //Enemy spawn methods
    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            SpawnEnemy();
        }
    }

    public Vector2 SpawnPos()
    {
         return new Vector2(UnityEngine.Random.Range(xMin, xMax), yPos);
    }
    
    public void SpawnEnemy()
    {
        Instantiate(enemyPrefab, SpawnPos(), enemyPrefab.transform.rotation);
    }

}
