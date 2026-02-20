using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadMovement : MonoBehaviour
{
    [SerializeField] private float roadSpeed;
    [SerializeField] private GameObject roadPrefab;
    private Vector3 spawnPosition = new Vector3(0f, 13f, 0f);
    private void FixedUpdate()
    {
        transform.Translate(0f, roadSpeed * Time.fixedDeltaTime, 0f);
        if (transform.position.y < -13f)
        {
            Instantiate(roadPrefab, spawnPosition, Quaternion.identity).name = "road";
            Destroy(gameObject);
        }
    }
}
