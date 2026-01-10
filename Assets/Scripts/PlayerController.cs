using System;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject playerCamera;
    [SerializeField] private float walkSpeed, sprintSpeed, mouseSensitivity, jumpForce, smoothTime;
    private float _verticalLookRotation;
    private bool _isGround;
    private Vector3 _smoothMove;
    private Vector3 _moveAmount;
    private Rigidbody _rb;
    private PhotonView _pnView;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _pnView = GetComponent<PhotonView>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        if (!_pnView.IsMine)
        {
            Destroy(playerCamera);
        }
    }

    private void Update()
    {
        if (!_pnView.IsMine)
        {
            return;
        }
        Look();
        Move();
    }

    private void FixedUpdate()
    {
        if (!_pnView.IsMine)
        {
            return;
        }
        _rb.MovePosition(_rb.position + transform.TransformDirection(_moveAmount) * Time.fixedDeltaTime);
    }

    private void Look()
    {
        transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * mouseSensitivity);
        _verticalLookRotation +=  Input.GetAxis("Mouse Y") * mouseSensitivity;
        _verticalLookRotation = Mathf.Clamp(_verticalLookRotation, -80f, 90f);
        
        playerCamera.transform.localEulerAngles = Vector3.left * _verticalLookRotation;
    }
    private void Move()
    {
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveDirection.Normalize();
        _moveAmount = Vector3.SmoothDamp(_moveAmount,
            moveDirection * (Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed),
            ref _smoothMove,
            smoothTime);
    }
}
