using UnityEngine;
using Photon.Pun;
using TMPro;

public class SetPlayerName : MonoBehaviourPunCallbacks
{
  [SerializeField] private TMP_InputField nicknameField;

  public override void OnConnectedToMaster()
  {
    LoadNickname();
  }
  
  private void LoadNickname()
  {
    string playerName = PlayerPrefs.GetString("SaveNickName");
    if (string.IsNullOrEmpty(playerName))
    {
      playerName = $"Player_{Random.Range(0, 10000)}";
    }
    PhotonNetwork.NickName = playerName;
    nicknameField.text = playerName;
  }

  public void ChangeNickname()
  {
    PlayerPrefs.SetString("SaveNickName", nicknameField.text);
    LoadNickname();
  }
}