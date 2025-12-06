using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyWaveSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs; //змінну для префаба ворогів
    [SerializeField] private GameObject[] enemyBossPrefabs; //змінну для префаба ворогів
    [SerializeField] private Transform spawnPoint;  //змінну для збереженя точки спавну
    [Header("Time between waves")]
    [SerializeField] private float countdown = 3f; //змінну для часу між хвилями
    [Header("Time between each enemy spawn")]
    [SerializeField] private float timeBetweenSpawnEnemy = 1f; //змінну для часу між спавном кожного ворога
    private int waveNumber; //номер хвилі по порядку

    private void Start()
    {
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        yield return new WaitForSeconds(countdown);
        for (int i = 0; i < waveNumber; i++)
        {
            GameObject enemyForSpawn = enemyPrefabs[Random.Range(0, enemyPrefabs.Length - 1)];
            if (waveNumber % 3 == 0)
            {
                enemyForSpawn = enemyBossPrefabs[Random.Range(0, enemyBossPrefabs.Length - 1)];
            }
            Instantiate(enemyForSpawn, spawnPoint.position, Quaternion.identity);
            yield return new WaitForSeconds(timeBetweenSpawnEnemy);
        }

        waveNumber++;
        StartCoroutine(SpawnWave());
    }
}
