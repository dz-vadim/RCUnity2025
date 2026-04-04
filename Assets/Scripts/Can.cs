using System;
using UnityEngine;

public class Can : MonoBehaviour
{
    [SerializeField] private GameObject particles;

    private void Start()
    {
        particles = Resources.Load<GameObject>("Particle");
    }

    private void OnMouseDown()
    {
        Instantiate(particles, transform.position, Quaternion.identity);
    }
    private void Update()
    {
        if (transform.position.y < -1)
        {
            Destroy(gameObject);
        }
    }
}
