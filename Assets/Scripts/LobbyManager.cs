
using System;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    private void Awake()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnJoinedLobby()
    {
        print("Connected to lobby");
        WindowManager.Layout.OpenLayout("MainMenu");
        GameObject.Find("StatisticsText").GetComponent<TextMeshProUGUI>().text =
            $"Players online: {PhotonNetwork.CountOfPlayersOnMaster}";
    }
}
