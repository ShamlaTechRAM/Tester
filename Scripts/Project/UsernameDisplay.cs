using UnityEngine;
using Photon.Pun;
using TMPro;

public class UsernameDisplay : MonoBehaviour
{
    [SerializeField] PhotonView _photonView;
    [SerializeField] TMP_Text nameText;    

    void Start()
    {
        if (_photonView.IsMine) {
            gameObject.SetActive(false);
        }

        nameText.text = _photonView.Owner.NickName;
    }
    
}
