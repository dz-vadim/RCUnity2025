using Photon.Pun;
using System.IO;
using UnityEngine;

public class PlayerManager : MonoBehaviourPunCallbacks
{
    private PhotonView _pnView;
    private GameObject _controller;
    private void Awake()
    {
        _pnView = GetComponent<PhotonView>();
    }

    private void Start()
    {
        if (_pnView.IsMine)
        {
            CreateController();
        }
    }

    private void CreateController()
    {
        _controller = PhotonNetwork.Instantiate(Path.Combine("PlayerController"), 
                                    Vector3.zero, Quaternion.identity, 0, new object[]{_pnView.ViewID});
    }

    public void Die()
    {
        PhotonNetwork.Destroy(_controller);
        CreateController();
    }
}