using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour {

    public Transform environmentTransform;
    public GameObject[] rumahAdatPrefabs;

    public Vector3[] spawnPoints;
    
    public float maxX;
    public float maxY;
    public float minX;
    public float minY;

    void Start() {
        int selectedPlace = (int)PhotonNetwork.CurrentRoom.CustomProperties["roomPlace"];

        Instantiate(rumahAdatPrefabs[selectedPlace], environmentTransform);

        Vector3 randomPosition = new Vector3(Random.Range(minX, maxX), 1, Random.Range(minY, maxY));

        int playerAvatarIndex = (int)PhotonNetwork.LocalPlayer.CustomProperties["playerAvatar"];
        PlayerPrefs.SetInt("PlayerAvatar", playerAvatarIndex);
        string prefabName = PlayerAvatars.Instance.GetPlayerAvatarsName(playerAvatarIndex);

        PhotonNetwork.Instantiate(prefabName, spawnPoints[selectedPlace], Quaternion.identity);
    }

}
