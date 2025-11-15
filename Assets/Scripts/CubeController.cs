using System;
using UnityEngine;
public class CubeController : MonoBehaviour
{
    private void FixedUpdate()
    {
        MoveWithRigidbodyVelocity();
    }
    private void MoveWithTransformPosition()
    {
        Vector3 position = transform.position;
        if (Input.GetKey(KeyCode.W)) position.z += 0.01f;
        if (Input.GetKey(KeyCode.S)) position.z -= 0.01f;
        if (Input.GetKey(KeyCode.A)) position.x += 0.01f;
        if (Input.GetKey(KeyCode.D)) position.x -= 0.01f;
        
        if (Input.GetKey(KeyCode.Space)) position.y += 0.1f;
        transform.position = position;
    } 

    private void MoveWithTransformTranslate()
    {
        float speed = 5f;
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * Time.fixedDeltaTime  * speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * Time.fixedDeltaTime * speed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * Time.fixedDeltaTime * speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * Time.fixedDeltaTime * speed);
        }
    }

    private void MoveWithRigidbodyVelocity()
    {
        float speed = 5f;
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(moveX * speed, rb.velocity.y, moveZ * speed);
    }
}
