using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class Launcher : MonoBehaviourPunCallbacks
{
    private const byte MaxPlayersPerRoom = 4;

    [SerializeField] private GameObject _controlPanel;
    [SerializeField] private GameObject _progressLabel;

    private string _gameVersion = "1";
    private bool _isConnecting;

    private void Awake() => PhotonNetwork.AutomaticallySyncScene = true;

    private void Start()
    {
        _progressLabel.SetActive(false);
        _controlPanel.SetActive(true);

        _isConnecting = PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.GameVersion = _gameVersion;
    }

    public override void OnConnectedToMaster()
    {
        if (_isConnecting)
        {
            Debug.Log("Launcher: OnConnectedToMaster() was called by PUN");
            PhotonNetwork.JoinLobby();
            _isConnecting = false;
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        _progressLabel.SetActive(false);
        _controlPanel.SetActive(true);

        Debug.LogWarningFormat("Launcher: OnDisconnected() was called by PUN with reason {0}", cause);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.LogWarningFormat("Launcher:OnJoinRandomFailed() was called by PUN. No random room available");
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(Scenes.Room);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
    }

    public void Connect(string roomName)
    {
        _progressLabel.SetActive(true);
        _controlPanel.SetActive(false);

        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRoom(roomName);
        }
        else
        {
            _isConnecting = PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = _gameVersion;
        }
    }

    public void Create(string roomName)
    {
        _progressLabel.SetActive(true);
        _controlPanel.SetActive(false);

        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.CreateRoom(roomName, new RoomOptions { MaxPlayers = MaxPlayersPerRoom });
            Debug.Log("Launcher: Create");
        }
        else
        {
            _isConnecting = PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = _gameVersion;
        }
    }
}
