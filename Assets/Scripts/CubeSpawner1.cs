using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner1 : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoint;
    [SerializeField] private GameObject itemPrefab;
    Vector3 spawnPosition;

    private void Start()
    {
        spawnPosition = transform.position;
        InvokeRepeating(nameof(SpawnItem), 0, 1f);
    }

    private void SpawnItem()
    {
        int index  = (int)Random.Range(0, spawnPoint.Length);
        spawnPosition = spawnPoint[index].position;
        GameObject item = Instantiate(itemPrefab, spawnPosition, Quaternion.identity);
        Rigidbody rb = item.GetComponent<Rigidbody>();
    }
}
