using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using System.Text.RegularExpressions;
using TMPro;
using Photon.Pun;

public class ChatManager : MonoBehaviour
{
    private PhotonView photonView;
    private List<string> messages = new List<string>();
    private float buildDelay = 0f;
    private int maximumMessages = 14;

    public TMP_InputField chatInput;
    public TextMeshProUGUI chatContent;

    public GameObject chatBubblePrefab;
    public GameObject content;
    public ScrollRect contentScrollRect;

    void Start() {
        photonView = GetComponent<PhotonView>();
    }

    void Update() {
        if (PhotonNetwork.InRoom) {
            chatContent.maxVisibleLines = maximumMessages;
            if (messages.Count > maximumMessages) {
                messages.RemoveAt(0);
            }
            if (buildDelay < Time.time) {
                BuildChatContents();
                buildDelay = Time.time + 0.25f;
            }
        } else if (messages.Count > 0) {
            messages.Clear();
            chatContent.text = "";
        }

        if (Keyboard.current.enterKey.wasReleasedThisFrame) {
            if (EventSystem.current.currentSelectedGameObject == chatInput) {
                //Debug.Log("Test 1");
                EventSystem.current.SetSelectedGameObject(chatInput.gameObject, null);
                chatInput.OnPointerClick(new PointerEventData(EventSystem.current));
            } else {
                //Debug.Log("Test 2");
                SubmitChat();
            }
        }
    }

    [PunRPC]
    void RPC_AddNewMessage(string nickName, string msg) {
        //messages.Add(msg);
        SpawnChatBubble(nickName, msg);
        BottomIconClick.newChatCheck = true;
    }

    public void SendChat(string msg) {
        //string newMessage = "<b>" + PhotonNetwork.NickName + "</b> : " + msg;
        //string newMessage = msg;
        photonView.RPC("RPC_AddNewMessage", RpcTarget.All, PhotonNetwork.NickName, msg);
    }

    public void SubmitChat() {
        string blankCheck = chatInput.text;
        blankCheck = Regex.Replace(blankCheck, @"\s", "");
        if (blankCheck == "") {
            chatInput.DeactivateInputField();
            chatInput.text = "";
            return;
        }

        SendChat(chatInput.text);
        chatInput.DeactivateInputField();
        chatInput.text = "";
    }

    void BuildChatContents() {
        string newContents = "";
        foreach (string s in messages) {
            newContents += s + "\n";
        }
        chatContent.text = newContents;
    }

    void SpawnChatBubble(string nickName, string msg) {
        float backup = contentScrollRect.verticalNormalizedPosition;
        GameObject instance = Instantiate(chatBubblePrefab, new Vector3(0, 0, 0), Quaternion.identity);
        instance.transform.Find("NickName").GetComponent<TextMeshProUGUI>().text = nickName;
        instance.transform.Find("Message").GetComponent<TextMeshProUGUI>().text = msg;
        instance.transform.SetParent(content.transform);
        instance.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
        StartCoroutine(ApplyScrollPosition(contentScrollRect, backup));
    }

    IEnumerator ApplyScrollPosition(ScrollRect contentScrollRect, float verticalPos) {
        yield return new WaitForEndOfFrame();
        contentScrollRect.verticalNormalizedPosition = verticalPos;
        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)contentScrollRect.transform);
    }
}
