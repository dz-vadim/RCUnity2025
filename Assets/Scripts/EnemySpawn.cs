using System;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    private void Start()
    {
        InvokeRepeating(nameof(Spawn), 0f, 1f);
    }
    private void Spawn()
    {
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }
}
