using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class UserRoomPanel : MonoBehaviourPunCallbacks
{
    public TMP_Text nameText;

    public Player Player { get; private set; }

    ExitGames.Client.Photon.Hashtable playerProperties = new ExitGames.Client.Photon.Hashtable();
    public RawImage playerAvatar;
    public RenderTexture[] renderTextureAvatars;

    public GameObject prevButton;
    public GameObject nextButton;

    public void SetPlayer(Player player) {
        Player = player;
        nameText.text = player.NickName;
        UpdatePlayerItem(Player);
    }

    public void SetNameTextUI(string name) {
        nameText.text = name;
    }

    public void OnPrevButton_Clicked() {
        if ((int)playerProperties["playerAvatar"] == 0) {
            playerProperties["playerAvatar"] = renderTextureAvatars.Length - 1;
        } else {
            playerProperties["playerAvatar"] = (int)playerProperties["playerAvatar"] - 1;
        }
        
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
    }

    public void OnNextButton_Clicked() {
        if ((int)playerProperties["playerAvatar"] == renderTextureAvatars.Length - 1) {
            playerProperties["playerAvatar"] = 0;
        }
        else {
            playerProperties["playerAvatar"] = (int)playerProperties["playerAvatar"] + 1;
        }

        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
    }


    public void ApplyLocalChange() {
        prevButton.SetActive(true);
        nextButton.SetActive(true);
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps) {
        if (Player == targetPlayer) {
            UpdatePlayerItem(targetPlayer);
        }
    }

    private void UpdatePlayerItem(Player player) {
        if (player.CustomProperties.ContainsKey("playerAvatar")) {
            playerAvatar.texture = renderTextureAvatars[(int)player.CustomProperties["playerAvatar"]];
            playerProperties["playerAvatar"] = (int)player.CustomProperties["playerAvatar"];
        } else {
            playerProperties["playerAvatar"] = 0;
        }
    }
}
