using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using UnityEngine;

public class Launcher : MonoBehaviourPunCallbacks
{
    [SerializeField] private PanelsSwitcher _panelsSwitcher;

    private const byte MaxPlayersPerRoom = 4;

    public bool IsConnecting {  get; private set; }

    private Coroutine _connectingWaiter;

    private void Awake() => PhotonNetwork.AutomaticallySyncScene = true;

    public override void OnConnectedToMaster()
    {
        if (_connectingWaiter != null)
        {
            StopCoroutine(_connectingWaiter);
            _panelsSwitcher.HideProgressPanel();
        }

        if (IsConnecting)
        {
            Debug.Log("Launcher: OnConnectedToMaster() was called by PUN");
            IsConnecting = false;
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        _panelsSwitcher.HideProgressPanel();
        Debug.LogWarningFormat("Launcher: OnDisconnected() was called by PUN with reason {0}", cause);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.LogWarningFormat("Launcher:OnJoinRandomFailed() was called by PUN. No random room available");
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = MaxPlayersPerRoom });
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(Scenes.Room);
    }

    public void Connect()
    {
        _panelsSwitcher.ShowProgressPanel();
        TryConnecting();
        PhotonNetwork.JoinRandomRoom();
    }

    public void Create(string roomName)
    {
        _panelsSwitcher.ShowProgressPanel();
        TryConnecting();
        PhotonNetwork.CreateRoom(roomName, new RoomOptions { MaxPlayers = MaxPlayersPerRoom });
    }

    public void FindGame(string roomName)
    {
        _panelsSwitcher.ShowProgressPanel();
        TryConnecting();
        PhotonNetwork.JoinRoom(roomName);
    }

    public void TryConnecting()
    {
        if (PhotonNetwork.IsConnected == false)
        {
            IsConnecting = PhotonNetwork.ConnectUsingSettings();
            _connectingWaiter = StartCoroutine(ConnectingWaiter());
        }
    }

    private IEnumerator ConnectingWaiter()
    {
        _panelsSwitcher.ShowProgressPanel();

        float seconds = 5f;
        var waitTime = new WaitForEndOfFrame();

        float passedTime = 0;

        while(passedTime < seconds)
        {
            passedTime += Time.deltaTime;
            yield return waitTime;
        }

        if (passedTime >= seconds)
        {
            _panelsSwitcher.HideProgressPanel();
            yield break;
        }
    }
}
