using System;
using UnityEngine;

public class Can : MonoBehaviour
{
    [SerializeField] private GameObject particles;
    private void OnMouseDown()
    {
        Instantiate(particles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void Update()
    {
        if (transform.position.y < -1)
        {
            FindObjectOfType<GameManager>().Damage(1);
        }
    }
}
