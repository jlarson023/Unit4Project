using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    //enemy spawn position variables
    public float xMin;
    public float xMax;
    public float yPos;
    //Enemy variables
    public int waveNumber = 1;
    public int enemyCount;
    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(waveNumber);
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<EnemyController>().Length;

        if (enemyCount == 0)
        {
            waveNumber++;
            SpawnEnemyWave(waveNumber);
        }
    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            SpawnEnemy();
        }
    }

    public Vector2 SpawnPos()
    {
         return new Vector2(Random.Range(xMin, xMax), yPos);
    }
    
    public void SpawnEnemy()
    {
        Instantiate(enemyPrefab, SpawnPos(), enemyPrefab.transform.rotation);
    }

}
