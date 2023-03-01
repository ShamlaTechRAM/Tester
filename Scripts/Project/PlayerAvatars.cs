using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAvatars : MonoBehaviour
{
    private static PlayerAvatars _instance;
    public static PlayerAvatars Instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType<PlayerAvatars>();
            }
            return _instance;
        }
    }
    [SerializeField]

    private List<GameObject> playerAvatars = new List<GameObject>();
    [SerializeField] private RenderTexture[] playerAvatarImages;

    public string GetPlayerAvatarsName(int index) {
        return playerAvatars[index].name;
    }

    public RenderTexture GetPlayerAvatarImage(int index) {
        return playerAvatarImages[index];
    }
}
