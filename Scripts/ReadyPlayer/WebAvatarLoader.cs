using System.Collections.Generic;
using StarterAssets;
using ReadyPlayerMe;
using UnityEngine;
using Photon.Pun;
using System;

[Serializable]
public struct ActorURLPair
{
    public string actor;
    public string url;
}

public class WebAvatarLoader : MonoBehaviour
{
    [SerializeField] LoadCanvas webViewPanel;
    private const string TAG = nameof(WebAvatarLoader);
    [SerializeField] private GameObject CharacterPrefab;
    [SerializeField] private RuntimeAnimatorController controller;
    private GameObject avatar, Character;
    private string avatarUrl = "";
    private ThirdPersonController tpController;
    private AvatarLoader avatarLoader;
    private Transform player;
    private int actor = 1, flag;
    public List<ActorURLPair> actorURLPairs = new();

    private void Start()
    {
//#if !UNITY_EDITOR && UNITY_WEBGL
//        PartnerSO partner = Resources.Load<PartnerSO>("Partner");
        
//        WebInterface.SetupRpmFrame(partner.Subdomain);
//#endif
    }

    //public void OnWebViewAvatarGenerated(string generatedUrl)
    //{
    //    AvatarLoader avatarLoader = new AvatarLoader();
    //    avatarUrl = generatedUrl;
    //    avatarLoader.OnCompleted += OnAvatarLoadCompleted;
    //    avatarLoader.OnFailed += OnAvatarLoadFailed;
    //    avatarLoader.LoadAvatar(avatarUrl);
    //}

    #region ExeTest
    public void LoadAvatarURL(string url)
    {         
        foreach(var entry in actorURLPairs)
        {
            if (entry.url.Equals(url))
            {
                flag++;
                avatar = Instantiate(PhotonView.Find(int.Parse(entry.actor)).transform.GetChild(1).gameObject);
                avatarUrl = url;
                AnimatorSetter();
            }
        }
        actorURLPairs.Add(new ActorURLPair { actor = actor++ + "001", url = url});
        if(flag < 1)
        {
            avatarLoader = new AvatarLoader();
            avatarUrl = url;
            avatarLoader.OnCompleted += OnAvatarLoadCompleted;
            avatarLoader.OnFailed += OnAvatarLoadFailed;
            avatarLoader.LoadAvatar(avatarUrl);
        }
        flag = 0;
    }
    #endregion ExeTest

    private void OnAvatarLoadCompleted(object sender, CompletionEventArgs args)
    {
        avatar = args.Avatar;
        AnimatorSetter();
    }

    private void OnAvatarLoadFailed(object sender, FailureEventArgs args)
    {
        webViewPanel.ToggleLoadScreen(false, true);
        SDKLogger.Log(TAG, $"Avatar Load failed with error: {args.Message}");
    }

    private void AnimatorSetter()
    {
        if (avatar != null && avatar.GetComponent<Animator>().runtimeAnimatorController == null)
            avatar.GetComponent<Animator>().runtimeAnimatorController = controller;
        PhotonInstantiator();
    }

    private void PhotonInstantiator()
    {
        Character = PhotonNetwork.Instantiate(CharacterPrefab.name, Vector3.zero, Quaternion.identity);
        player = Character.transform.GetChild(0);
        player.SetPositionAndRotation(new Vector3(UnityEngine.Random.Range(0, 5), 0, UnityEngine.Random.Range(0, 5)), Quaternion.Euler(0, UnityEngine.Random.Range(0, 180), 0));
        avatar.transform.SetParent(player, false);
        CharacterControlEnabler();
    }

    private void CharacterControlEnabler()
    {
        tpController = player.GetComponent<ThirdPersonController>();
        tpController.Avatar = avatar;
        tpController.avatarUrl = avatarUrl;
        webViewPanel.ToggleLoadScreen(false, false);
    }
}