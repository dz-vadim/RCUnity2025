using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject itemPrefab;
    Vector3 spawnPosition;

    private void Start()
    {
        spawnPosition = transform.position;
        InvokeRepeating(nameof(SpawnItem), 0, 1f);
    }

    private void SpawnItem()
    {
        spawnPosition.x = Random.Range(-1.5f, 1.5f);
        GameObject item = Instantiate(itemPrefab, spawnPosition, Quaternion.identity);
        Rigidbody rb = item.GetComponent<Rigidbody>();
        Vector3 direction = transform.position + Vector3.up * 10f;
        direction -= spawnPosition;
        rb.AddForce(direction, ForceMode.Impulse);
        rb.angularVelocity = Random.insideUnitSphere * 5f;
    }
}
