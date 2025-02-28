using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class DiceRollMediator : MonoBehaviourPun, IOnEventCallback
{
    [SerializeField] private Button _rollButton;
    [SerializeField] private Transform _throwPoint;

    private Player _player;
    private Dice _dice;
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
        Hashtable props = new()
        {
            { Events.RollDiceButtonClicked, _player.DicePosition}
        };

        PhotonNetwork.CurrentRoom.SetCustomProperties(props);
    }

    public void Initialize(ISpawnKeeper spawnKeeper)
    {
        _dice = spawnKeeper.Dice;
        _player = spawnKeeper.Player;
    }

    public void OnEvent(EventData photonEvent)
    {
        if (photonEvent.Code == (byte)EventCode.PropertiesChanged)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue(Events.RollDiceButtonClicked, out var value))
                {
                    _dice.Throw((Vector3)value);
                }
            }
        }
    }
}
