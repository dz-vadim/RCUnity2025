using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoint;
    [SerializeField] private GameObject bombPrefab;

    private void Start()
    {
        InvokeRepeating("Spawn", 1f, 2f);
    }
    private void Spawn()
    {
        Instantiate(bombPrefab, spawnPoint[Random.Range(0, spawnPoint.Length)].position, Quaternion.identity);
    }
}
