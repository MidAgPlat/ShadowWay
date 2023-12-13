using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject enemy;

    public int maxEnemyCount;
    private int enemyCount = 0;
    private bool spawnWait = false;

    void Start()
    {

    }

    void Update()
    {
        if(enemyCount<maxEnemyCount && !spawnWait)
        {
            int randSpawnPoint = Random.Range(0, spawnPoints.Length);
            Instantiate(enemy, spawnPoints[randSpawnPoint].position, Quaternion.identity);
            StartCoroutine(CoolDown());
        }
    }

    private IEnumerator CoolDown()
    {
        spawnWait = true;
        yield return new WaitForSeconds(Random.Range(3f, 10f));
        spawnWait = false;
    }
}
