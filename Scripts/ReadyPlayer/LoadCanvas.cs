using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class LoadCanvas : MonoBehaviour
{
    [SerializeField] private Button createAvatarButton;
    [SerializeField] private GameObject LoadingScreen;
    [SerializeField] private WebAvatarLoader loader;
    [SerializeField] private TMP_InputField field;

    private void Start()
    {
        if (createAvatarButton != null)
            createAvatarButton.onClick.AddListener(OnCreateAvatar);
    }

    private void OnCreateAvatar()
    {
        ToggleLoadScreen(true, false);
        //#if !UNITY_EDITOR && UNITY_WEBGL
        //                WebInterface.SetIFrameVisibility(true);
        //#endif
        loader.LoadAvatarURL(field.text);
    }

    public void ToggleLoadScreen(bool loadToggle, bool webViewToggle)
    {
        LoadingScreen.SetActive(loadToggle);
        gameObject.SetActive(webViewToggle);
    }
}