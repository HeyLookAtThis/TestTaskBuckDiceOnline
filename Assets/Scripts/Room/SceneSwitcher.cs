using Photon.Pun;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviourPunCallbacks
{
    public override void OnLeftRoom() => SceneManager.LoadScene(Scenes.Lobby);
    public void LeaveRoom() => PhotonNetwork.LeaveRoom();
}
