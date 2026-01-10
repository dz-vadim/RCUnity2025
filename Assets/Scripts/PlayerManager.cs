using Photon.Pun;
using System.IO;
using UnityEngine;

public class PlayerManager : MonoBehaviourPunCallbacks
{
    private PhotonView _pnView;
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
        PhotonNetwork.Instantiate(Path.Combine("PlayerController"), 
                                    Vector3.zero, Quaternion.identity);
    }
}