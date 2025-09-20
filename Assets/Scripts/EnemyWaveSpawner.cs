using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveSpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab; //змінну для префаба ворогів
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
            Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            yield return new WaitForSeconds(timeBetweenSpawnEnemy);
        }

        waveNumber++;
        StartCoroutine(SpawnWave());
    }
}
