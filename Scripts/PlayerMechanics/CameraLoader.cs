using UnityEngine;
using Photon.Pun;

public class CameraLoader : MonoBehaviour
{
    [SerializeField] PhotonView photonView;

    void Start()
    {
        if (!photonView.IsMine)
            Destroy(gameObject);
    }
}