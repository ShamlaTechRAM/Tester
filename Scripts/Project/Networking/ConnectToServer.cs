using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ConnectToServer : MonoBehaviourPunCallbacks {

    public GameObject roomGameObject;
    public GameObject lobbyGameObject;
    ExitGames.Client.Photon.Hashtable playerProperties = new ExitGames.Client.Photon.Hashtable();

    private void Awake() {
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
    }

    public override void OnConnectedToMaster() {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.NickName = UserManager.Instance.Name;
        PhotonNetwork.JoinLobby(TypedLobby.Default);

        if (PlayerPrefs.HasKey("PlayerAvatar")){
            playerProperties["playerAvatar"] = PlayerPrefs.GetInt("PlayerAvatar");
        } else {
            playerProperties["playerAvatar"] = 0;
        }

        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
    }

    public override void OnJoinedLobby() {
        roomGameObject.SetActive(false);
        lobbyGameObject.SetActive(true);
    }
}
