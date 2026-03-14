using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject cubePrefab;
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit raycastHit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out raycastHit))
            {
                SpawnCube(raycastHit.point);
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit raycastHit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out raycastHit))
            {
                if (raycastHit.collider.tag == "Finish")
                {
                    Destroy(raycastHit.collider.gameObject);
                }
            }
        }
    }

    private void SpawnCube(Vector3 position)
    {
        int pointX = (int)position.x;
        int pointY = (int)position.y + 1;
        int pointZ = (int)position.z;
        int size = 3;
        for (int x = pointX; x < pointX + size; x++)
        {
            for (int y = pointX; y < pointY + size; y++)
            {
                for (int z = pointZ; z < pointZ + size; z++)
                {
                    Instantiate(cubePrefab, new Vector3(x, y, z), Quaternion.identity);
                }
            }
        }
    }
}
