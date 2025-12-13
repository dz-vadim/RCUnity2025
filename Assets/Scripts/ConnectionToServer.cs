using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class ConnectionToServer : MonoBehaviourPunCallbacks
{
	public static ConnectionToServer Instance;
	[SerializeField] private TMP_InputField inputRoomName;
	[SerializeField] private TMP_Text roomName;
	[SerializeField] private Transform transformRoomList;
	[SerializeField] private GameObject roomItemPrefab;
	[SerializeField] private Transform transformPlayerList;
	[SerializeField] private GameObject playerItemPrefab;
	
	public void Awake()
	{
		Instance = this;
	}
	public override void OnConnectedToMaster()
	{
		PhotonNetwork.JoinLobby();
		PhotonNetwork.NickName = $"Player {Random.Range(0, 10000)}";
	}
	public void CreateNewRoom()
	{
		if (string.IsNullOrEmpty(inputRoomName.text))
		{
			return;
		}
		PhotonNetwork.CreateRoom(inputRoomName.text);
	}

	public override void OnJoinedRoom()
	{
		WindowManager.Layout.OpenLayout("GameRoom");
		roomName.text = PhotonNetwork.CurrentRoom.Name;
		Player[] players = PhotonNetwork.PlayerList;
		foreach (Transform child in transformPlayerList)
		{
			Destroy(child.gameObject);
		}
		for(int i = 0; i < players.Length; i++)
		{
			Instantiate(playerItemPrefab, transformPlayerList)
				.GetComponent<PlayerListItem>().SetUp(players[i]);
		}
	}

	public override void OnPlayerEnteredRoom(Player newPlayer)
	{
		Instantiate(playerItemPrefab, transformPlayerList)
			.GetComponent<PlayerListItem>().SetUp(newPlayer);
	}

	public void LeaveRoom()
	{
		PhotonNetwork.LeaveRoom();
	}

	public override void OnLeftRoom()
	{
		WindowManager.Layout.OpenLayout("MainMenu");
	}
	
	public override void OnRoomListUpdate(List<RoomInfo> roomList)
	{
		foreach (Transform child in transformRoomList)
		{
			Destroy(child.gameObject);
		}
		for(int i = 0; i < roomList.Count; i++)
		{
			Instantiate(roomItemPrefab, transformRoomList)
				.GetComponent<RoomItem>().SetUp(roomList[i]);
		}
	}
	public void JoinRoom(RoomInfo info)
	{
		PhotonNetwork.JoinRoom(info.Name);
	}
}