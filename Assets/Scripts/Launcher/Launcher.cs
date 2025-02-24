using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class Launcher : MonoBehaviourPunCallbacks
{
    [SerializeField] private PanelsSwitcher _panelsSwitcher;

    private const byte MaxPlayersPerRoom = 4;

    public bool IsConnecting {  get; private set; }

    private void Awake() => PhotonNetwork.AutomaticallySyncScene = true;

    public override void OnConnectedToMaster()
    {
        _panelsSwitcher.HideProgressPanel();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        _panelsSwitcher.HideProgressPanel();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = MaxPlayersPerRoom });
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(Scenes.Room);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
    }

    public override void OnLeftRoom()
    {
        _panelsSwitcher.HideProgressPanel();
    }

    public void JoinRandomRoom()
    {
        _panelsSwitcher.ShowLoadingPanel();
        PhotonNetwork.JoinRandomRoom();
    }

    public void Create(string roomName)
    {
        _panelsSwitcher.ShowLoadingPanel();
        PhotonNetwork.CreateRoom(roomName, new RoomOptions { MaxPlayers = MaxPlayersPerRoom });
    }

    public void FindGame(string roomName)
    {
        _panelsSwitcher.ShowLoadingPanel();
        PhotonNetwork.JoinRoom(roomName);
    }

    public void TryConnectingToMaster()
    {
        if (PhotonNetwork.IsConnected == false)
            IsConnecting = PhotonNetwork.ConnectUsingSettings();
    }
}
