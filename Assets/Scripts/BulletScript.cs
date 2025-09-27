using System;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    private Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>(); 
    }
    public void TakeForce(Transform target)
    {
        Vector3 direction = target.position - transform.position;
        rb.AddForce(direction * speed, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
        } 
        Destroy(gameObject);
    }
}
