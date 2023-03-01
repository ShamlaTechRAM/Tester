using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class BottomIconClick : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject chatbox;
    private bool chatboxToggle;
    private SoundInput soundInput;
    private Image chatIconImage;
    private Image micIconImage;

    public GameObject micButton, chatboxButton, shareButton, voiceManager;

    public static bool newChatCheck;

    [Header("Mic Icon")]
    public GameObject micIcon;
    public Sprite micOn, micOff;

    [Header("Chat Icon")]
    public GameObject chatIcon;
    public Sprite chatNormal, chatNotification;
    

    void Awake() {
        chatboxToggle = false;
        chatbox.SetActive(false);
        newChatCheck = false;
        soundInput = voiceManager.GetComponent<SoundInput>();
        chatIconImage = chatIcon.GetComponent<Image>();
        chatIconImage.sprite = chatNormal;
        micIconImage = micIcon.GetComponent<Image>();
        micIconImage.sprite = micOff;
    }

    void Update() {
        if (chatboxToggle == true) {
            chatbox.SetActive(true);
        } else {
            chatbox.SetActive(false);
        }

        if (newChatCheck == true) {
            chatIconImage.sprite = chatNotification;
        } else {
            chatIconImage.sprite = chatNormal;
        }
    }

    public void OnPressMicButton() {
        soundInput.buttonToggle = !soundInput.buttonToggle;
        if (soundInput.buttonToggle == true) {
            micIconImage.sprite = micOn;
            Debug.Log("Mic enabled");
        } else {
            micIconImage.sprite = micOff;
            Debug.Log("Mic disabled");
        }
        clickAnimation(micButton);
    }

    public void OnPressChatboxBottomButton() {
        chatboxToggle = !chatboxToggle;
        newChatCheck = false;
        clickAnimation(chatboxButton);
    }

    public void OnPressChatboxCloseButton() {
        chatboxToggle = false;
    }

    public void OnPressShareButton() {
        clickAnimation(shareButton);
    }

    public void OnPressExitButton() {
        PhotonNetwork.LeaveRoom();
    }

    public void clickAnimation(GameObject button) {
        LeanTween.scale(button, new Vector3(0.4f, 0.4f, 0.4f), 0.2f);
        LeanTween.scale(button, new Vector3(1f, 1f, 1f), 0.2f);
    }

    public override void OnLeftRoom() {
        PhotonNetwork.LoadLevel(0);
    }
}
