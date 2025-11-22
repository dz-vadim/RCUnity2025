using UnityEngine;

public class CapsuleController : MonoBehaviour
{
    private const float SPEED = 4f;
    private float moveX,  moveZ, moveY;
    private CharacterController controller;
    private Vector3 moveDirection;

    private void Start()
    {
        moveY = 0;
        controller = GetComponent<CharacterController>();
    }
    private void MoveWithCharacterController()
    {
        moveX = Input.GetAxis("Horizontal") * Time.fixedDeltaTime;
        moveZ = Input.GetAxis("Vertical") * Time.fixedDeltaTime;
        if (controller.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                moveY = 0.15f;
            }
        }
        else
        {
            moveY -= 1f * Time.fixedDeltaTime;
        }
        moveDirection = new Vector3(moveX, moveY, moveZ);
        controller.Move(moveDirection * SPEED);
    }

    private void FixedUpdate()
    {
        MoveWithCharacterController();
    }
}
