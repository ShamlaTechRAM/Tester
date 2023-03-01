using UnityEngine;
using UnityEngine.EventSystems;
using System.Runtime.InteropServices;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class OngoingRoomManager : MonoBehaviourPunCallbacks 
{

    [SerializeField] private PlayerListItem playerListItemPrefab;
    [SerializeField] private Transform playerListTransform;
    [SerializeField] private TMP_Text roomCodeText;
    [SerializeField] private TMP_Text participantCounterText;



    private void Start() {
        roomCodeText.text = "RoomCode: " + PhotonNetwork.CurrentRoom.Name;
        participantCounterText.text = PhotonNetwork.CurrentRoom.PlayerCount.ToString();
        Player[] players = PhotonNetwork.PlayerList;

        for (int i = 0; i < players.Length; i++) {
            Instantiate(playerListItemPrefab, playerListTransform).Setup(players[i]);
        }
    }
    private static void ReceivePaste(string str)
    {
        GUIUtility.systemCopyBuffer = str;
    }

    public override void OnPlayerEnteredRoom(Player newPlayer) {
        Instantiate(playerListItemPrefab, playerListTransform).Setup(newPlayer);
        participantCounterText.text = PhotonNetwork.CurrentRoom.PlayerCount.ToString();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer) {
        participantCounterText.text = PhotonNetwork.CurrentRoom.PlayerCount.ToString();
    }
    public void CopyToClipboard()
    {
        TextEditor textEditor = new TextEditor();
        textEditor.text = roomCodeText.text;
        textEditor.SelectAll();
        textEditor.Copy();
    }
}
