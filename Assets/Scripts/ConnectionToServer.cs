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
	
	public void Awake()
	{
		Instance = this;
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
}