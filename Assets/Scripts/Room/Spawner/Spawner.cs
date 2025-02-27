using Cinemachine;
using Photon.Pun;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviourPunCallbacks, ISpawnKeeper
{
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    [SerializeField] private Transform _throwPoint;
    [SerializeField] private List<Transform> _diceSpawnPoints;

    private Dice _dice;
    private Player _player;
    private List<PlayerSpawnPoint> _playerSpawnPoints;

    public Dice Dice => _dice;
    public Player Player => _player;

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        if(PhotonNetwork.IsMasterClient)
            CreatePlayer();
    }

    public void Run()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            CreateDice();
            CreatePlayerSpawnPoints();
            CreatePlayer();
        }
    }

    public void CreateDice()
    {
        DiceFactory factory = new();
        _dice = factory.Get(_diceSpawnPoints[0].position);
    }

    public void CreatePlayer()
    {
        PlayerFactory factory = new();
        PlayerSpawnPoint spawnPoint = GetFreePlayerSpawnPoint();

        _player = factory.Get(spawnPoint.Position, spawnPoint.DicePosition, _throwPoint.position, _dice);

        if (spawnPoint.TryPlacePlayer(_player))
            _virtualCamera.Follow = spawnPoint.CameraTarget;
    }

    public void CreatePlayerSpawnPoints()
    {
        _playerSpawnPoints = new List<PlayerSpawnPoint>();
        PlayerSpawnPointsFactory pointsFactory = new();

        foreach (var point in _diceSpawnPoints)
            _playerSpawnPoints.Add(pointsFactory.Create(point.position));
    }

    private PlayerSpawnPoint GetFreePlayerSpawnPoint() => _playerSpawnPoints.First(point => point.IsEmpty);
}
