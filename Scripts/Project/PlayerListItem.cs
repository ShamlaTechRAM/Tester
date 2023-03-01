using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class PlayerListItem : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _roleText;
    [SerializeField] private RawImage _avatar;

    Player _player;

    public void Setup(Player player) {
        _player = player;
        _nameText.text = player.NickName;
        _roleText.text = player.IsMasterClient ? "Host" : "Participant";
    }

    public override void OnPlayerLeftRoom(Player otherPlayer) {
        if (_player == otherPlayer) {
            Destroy(gameObject);
        }
    }

    public override void OnLeftRoom() {
        Destroy(gameObject);
    }
}
