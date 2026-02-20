using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        transform.rotation = Quaternion.Euler(0f, 0, 90f);
        rb.AddForce(Vector2.up * speed);

        Destroy(gameObject, 10f);
    }
}
