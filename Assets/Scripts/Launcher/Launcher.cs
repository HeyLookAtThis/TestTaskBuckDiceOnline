using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using UnityEngine;

public class Launcher : MonoBehaviourPunCallbacks
{
    private const byte MaxPlayersPerRoom = 4;

    [SerializeField] private GameObject _controlPanel;
    [SerializeField] private GameObject _progressLabel;

    private bool _isConnecting;

    private Coroutine _connectingWaiter;

    private void Awake() => PhotonNetwork.AutomaticallySyncScene = true;

    private void Start()
    {
        ShowProgressPanel();
        TryConnected();
    }

    public override void OnConnectedToMaster()
    {
        if (_connectingWaiter != null)
        {
            StopCoroutine(_connectingWaiter);
            HideProgressPanel();
        }

        if (_isConnecting)
        {
            Debug.Log("Launcher: OnConnectedToMaster() was called by PUN");
            _isConnecting = false;
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        HideProgressPanel();
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
        ShowProgressPanel();
        TryConnected();
        PhotonNetwork.JoinRandomRoom();
    }

    public void Create(string roomName)
    {
        ShowProgressPanel();
        TryConnected();
        PhotonNetwork.CreateRoom(roomName, new RoomOptions { MaxPlayers = MaxPlayersPerRoom });
    }

    private void TryConnected()
    {
        if (PhotonNetwork.IsConnected == false)
        {
            _isConnecting = PhotonNetwork.ConnectUsingSettings();
            _connectingWaiter = StartCoroutine(ConnectingWaiter());
        }
    }

    private IEnumerator ConnectingWaiter()
    {
        float seconds = 5f;
        var waitTime = new WaitForEndOfFrame();

        float passedTime = 0;

        while(passedTime < seconds)
        {
            passedTime += Time.deltaTime;
            Debug.Log(passedTime);
            yield return waitTime;
        }

        if (passedTime >= seconds)
        {
            HideProgressPanel();
            yield break;
        }
    }

    private void ShowProgressPanel()
    {
        _progressLabel.SetActive(true);
        _controlPanel.SetActive(false);
    }

    private void HideProgressPanel()
    {
        _progressLabel.SetActive(false);
        _controlPanel.SetActive(true);
    }
}
