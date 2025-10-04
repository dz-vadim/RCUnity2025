using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject spawnPoint;
    private float 
        minX = -4.5f,
        maxX =  4.5f,
        minZ = -4.5f,
        maxZ = 2f;

    private void Start()
    {
        InvokeRepeating(nameof(ChangePosition), 3f, 1f);
    }

    private void ChangePosition()
    {
        float x = Random.Range(minX, maxX);
        float z = Random.Range(minZ, maxZ);
        spawnPoint.transform.position = new Vector3(x, transform.position.y, z);
    }
}
