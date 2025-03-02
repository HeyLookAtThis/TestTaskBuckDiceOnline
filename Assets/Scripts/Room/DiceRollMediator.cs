using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class DiceRollMediator : MonoBehaviourPun
{
    [SerializeField] private Button _rollButton;
    [SerializeField] private Transform _throwPoint;
    [SerializeField] private GameMover _gameMover;

    private Player _player;

    private void OnEnable()
    {
        _rollButton.onClick.AddListener(OnRollDice);
    }

    private void OnDisable()
    {
        _rollButton.onClick.RemoveListener(OnRollDice);
    }

    public void OnRollDice()
    {
        object[] data = new object[] { _player.DicePosition };
        RaiseEventOptions eventOptions = new() { Receivers = ReceiverGroup.MasterClient };
        PhotonNetwork.RaiseEvent(Events.RollDiceButtonClickedCode, data, eventOptions, SendOptions.SendUnreliable);
        _gameMover.ChangePlayer();
    }

    public void Initialize(ISpawnKeeper spawnKeeper)
    {
        _player = spawnKeeper.Player;
    }
}
