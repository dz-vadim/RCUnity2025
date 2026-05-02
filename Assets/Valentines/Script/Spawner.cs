using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject wall;

    private void Start()
    {
        InvokeRepeating("Spawn", 0.5f, 2.5f);
    }

    private void Spawn()
    {
        Vector3 spawnPosition = new Vector3(0f, Random.Range(-2.5f, 2.5f), 0f); 
        Destroy(Instantiate(wall, transform.position + spawnPosition, Quaternion.identity),10f);
    }
}
