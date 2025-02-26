using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class RoomBootstrap : MonoBehaviourPunCallbacks
{
    [SerializeField] private Transform _diceSpawnPoint;
    [SerializeField] private Transform _throwPoint;
    [SerializeField] private DiceRollMediator _mediator;

    private Dice _dice;
    private PlayerAvatar _playerAvatar;

    private void Awake()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            SpawnDice();
        }

        SpawnPlayer();
        _mediator.Initialize(_playerAvatar);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
    }

    private void SpawnDice()
    {
        DiceFactory factory = new();
        _dice = factory.Get(_diceSpawnPoint.position);
    }

    private void SpawnPlayer()
    {
        _playerAvatar = PhotonNetwork.Instantiate(Prefabs.PlayerAvatar, Vector3.zero, Quaternion.identity).GetComponent<PlayerAvatar>();
        _playerAvatar.Initialize(_diceSpawnPoint.position, _throwPoint.position, _dice);
    }
}
