using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class DiceRollMediator : MonoBehaviourPun, IOnEventCallback
{
    [SerializeField] private Button _rollButton;

    private Player _player;

    private void OnEnable()
    {
        _rollButton.onClick.AddListener(OnRollDice);
        PhotonNetwork.NetworkingClient.EventReceived += OnEvent;
    }

    private void OnDisable()
    {
        _rollButton.onClick.RemoveListener(OnRollDice);
        PhotonNetwork.NetworkingClient.EventReceived -= OnEvent;
    }

    private void OnRollDice()
    {
        Hashtable props = new Hashtable
            {
                { "Event", "DoSomething" }
            };
        PhotonNetwork.CurrentRoom.SetCustomProperties(props);
    }

    public void Initialize(ISpawnKeeper spawnKeeper)
    {
        _player = spawnKeeper.Player;
    }

    public void OnEvent(EventData photonEvent)
    {
        if (photonEvent.Code == (byte)EventCode.PropertiesChanged)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue("Event", out var value))
                {
                    if ((string)value == "DoSomething")
                    {
                        _player.ThrowDice();
                    }
                }
            }
        }
    }
}
