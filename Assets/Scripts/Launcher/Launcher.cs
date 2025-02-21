using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class Launcher : MonoBehaviourPunCallbacks
{
    private const byte MaxPlayersPerRoom = 4;

    [SerializeField] private GameObject _controlPanel;
    [SerializeField] private GameObject _progressLabel;

    private string _gameVersion = "1";

    private void Awake() => PhotonNetwork.AutomaticallySyncScene = true;

    private void Start()
    {
        _progressLabel.SetActive(false);
        _controlPanel.SetActive(true);
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("PUN Basics Tutorial/Launcher: OnConnectedToMaster() was called by PUN");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        _progressLabel.SetActive(false);
        _controlPanel.SetActive(true);

        Debug.LogWarningFormat("PUN Basics Tutorial/Launcher: OnDisconnected() was called by PUN with reason {0}", cause);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("PUN Basics Tutorial/Launcher:OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = MaxPlayersPerRoom });
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("PUN Basics Tutorial/Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.");
    }

    public void Connect()
    {
        _progressLabel.SetActive(true);
        _controlPanel.SetActive(false);

        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = _gameVersion;
        }
    }    
}
