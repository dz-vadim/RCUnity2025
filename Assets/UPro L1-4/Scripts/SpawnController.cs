using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private GameObject NPCPrefab;

    private void OnMouseDown()
    {
        Instantiate(NPCPrefab,  transform.position, Quaternion.identity);
    }
}
