using System.Collections.Generic;
using System.Collections;
using Photon.Realtime;
using UnityEngine;
using Photon.Pun;

public class MasterNetworkManager : MonoBehaviourPunCallbacks
{
    public List<RoomInfo> roomInfos = new List<RoomInfo>();

    private static MasterNetworkManager _instance;
    public static MasterNetworkManager Instance
    {
        get 
        {
            if (_instance == null) 
                _instance = FindObjectOfType<MasterNetworkManager>();
            return _instance;
        }
    }

    private void Awake()
    {
#if !UNITY_EDITOR && UNITY_WEBGL
        WebGLInput.captureAllKeyboardInput = true;
#endif
    }

    private void Start() {
        if (!PhotonNetwork.IsConnected) {
            PhotonNetwork.OfflineMode = false;
            PhotonNetwork.GameVersion = Application.version;
            PhotonNetwork.ConnectUsingSettings();
        }
        else
            StartCoroutine(LeaveAndReconnect());
    }
    public override void OnConnectedToMaster() {        
        PhotonNetwork.AutomaticallySyncScene = false;
        PhotonNetwork.NickName = UserManager.Instance.Name;
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {        
        roomInfos = roomList;
    }

    public override void OnJoinedRoom() {
        PhotonNetwork.LoadLevel(1);
    }

    IEnumerator LeaveAndReconnect() {
        while (PhotonNetwork.NetworkClientState != ClientState.Disconnected)
            yield return null;

        PhotonNetwork.OfflineMode = false;
        PhotonNetwork.GameVersion = Application.version;
        PhotonNetwork.ConnectUsingSettings();
    }
}