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
        object[] data = new object[] { _player.DicePosition };
        RaiseEventOptions eventOptions = new() { Receivers = ReceiverGroup.MasterClient };
        PhotonNetwork.RaiseEvent(Events.RollDiceButtonClickedCode, data, eventOptions, SendOptions.SendUnreliable);
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
                if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue(Events.RollDiceButtonClickedCode, out var value))
                {
                    _dice.RunThrower((Vector3)value);
                }
            }
        }
    }
}
