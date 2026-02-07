using System;
using UnityEngine;
using Photon.Pun;
using System.IO;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;
public class PlayerController : MonoBehaviourPunCallbacks, IDamageble
{
    [SerializeField] private GameObject playerCamera;
    [SerializeField] private float walkSpeed, sprintSpeed, mouseSensitivity, jumpForce, smoothTime;
    private float _verticalLookRotation;
    private bool _isGround;
    private Vector3 _smoothMove;
    private Vector3 _moveAmount;
    private Rigidbody _rb;
    private PhotonView _pnView;

    [SerializeField] private Item[] items;
    private int _itemIndex;
    private int _prevItemIndex = -1;
    
    private float maxHealth = 100f;
    private float currentHealth;
    private PlayerManager _playerManager;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _pnView = GetComponent<PhotonView>();
        _playerManager = PhotonView.Find((int)_pnView.InstantiationData[0]).GetComponent<PlayerManager>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        if (!_pnView.IsMine)
        {
            Destroy(playerCamera);
            Destroy(_rb);
        }
        else
        {
            EquipItem(0);
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
        Jump();
        SelectWeapon();
        UseItem();
    }

    private void UseItem()
    {
        items[_itemIndex].Use();
    }

    private void SelectWeapon()
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (Input.GetKeyDown((i + 1).ToString()))
            {
                EquipItem(i);
                break;
            }
        }
    }

    private void EquipItem(int  index)
    {
        if (index == _prevItemIndex)
        {
            return;
        }
        _itemIndex = index;
        items[_itemIndex].itemGameObject.SetActive(true);
        if (_prevItemIndex != -1)
        {
            items[_prevItemIndex].itemGameObject.SetActive(false);
        }
        _prevItemIndex = _itemIndex;
        Hashtable hash =  new Hashtable();
        hash.Add("index",  _itemIndex);
        PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if (!_pnView.IsMine && targetPlayer == _pnView.Owner)
        {
            EquipItem((int)changedProps["index"]);
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space)  && _isGround)
        {
            _rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
    }

    public void GroundState(bool isGround)
    {
        this._isGround = isGround;
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

    public override void OnJoinedRoom()
    {  
        //PhotonNetwork.Instantiate(Path.Combine("PlayerManager"), Vector3.zero, Quaternion.identity);
        print(PhotonNetwork.CurrentRoom.Players);
    }

    public void TakeDamage(float damage)
    {
        _pnView.RPC(nameof(RPC_Damage),  RpcTarget.All, damage);
    }

    [PunRPC]
    void RPC_Damage(float damage)
    {
        if(!_pnView.IsMine) return;
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            _playerManager.Die();
        }
    }
}
