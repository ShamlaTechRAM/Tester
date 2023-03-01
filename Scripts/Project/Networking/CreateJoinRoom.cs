using System.Collections.Generic;
using Photon.Realtime;
using System.Linq;
using UnityEngine;
using Photon.Pun;
using TMPro;

public enum LobbyState
{
    Create,
    Join,
}

public class CreateJoinRoom : MonoBehaviourPunCallbacks
{
    public LobbyState lobbyState;
    private string tempName;
    [SerializeField] private TMP_InputField JoinRoomCode;
    private static CreateJoinRoom _instance;
    
    public static CreateJoinRoom Instance
    {
        get 
        {
            if (_instance == null)
                _instance = FindObjectOfType<CreateJoinRoom>();
            
            return _instance;
        }
    }
    
    public void OnCreateRoomButton_Clicked() {
        UserManager.Instance.SetName(tempName);
        PhotonNetwork.NickName = tempName;

        lobbyState = LobbyState.Create;
        PhotonNetwork.CreateRoom(Random.Range(1000, 9999).ToString());
    }

    public void OnJoinRoomButton_Clicked() {
        UserManager.Instance.SetName(tempName);
        PhotonNetwork.NickName = tempName;

        lobbyState = LobbyState.Join;

        RoomInfo room = MasterNetworkManager.Instance.roomInfos.FirstOrDefault(r => r.Name == JoinRoomCode.text);
        PhotonNetwork.JoinRoom(JoinRoomCode.text);
    }

    public void SetTempName(TMP_InputField inputField)
    {
        tempName = inputField.text;        
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        base.OnRoomListUpdate(roomList);
    }
}