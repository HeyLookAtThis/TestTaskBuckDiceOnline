using Cinemachine;
using ExitGames.Client.Photon;
using ExitGames.Client.Photon.StructWrapping;
using Photon.Pun;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviourPunCallbacks, ISpawnKeeper
{
    [SerializeField] private GameMover _gameMover;
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    [SerializeField] private Transform _throwPoint;
    [SerializeField] private List<Transform> _diceSpawnPoints;

    private Player _player;
    private List<PlayerSpawnPoint> _playerSpawnPoints;

    public Player Player => _player;

    public void Run()
    {
        if (PhotonNetwork.IsMasterClient)
            CreateDice();

        CreaterSpawnPoints();
        CreatePlayer(GetFreePlayerSpawnPoint());
    }

    private void CreateDice()
    {
        DiceFactory factory = new();
        factory.Get(_throwPoint.position);
    }

    private void CreatePlayer(PlayerSpawnPoint spawnPoint)
    {
        PlayerFactory factory = new();
        _player = factory.Get(spawnPoint.Position, spawnPoint.DicePosition);

        _virtualCamera.Follow = spawnPoint.CameraTarget;
    }

    private void CreaterSpawnPoints()
    {
        _playerSpawnPoints = new List<PlayerSpawnPoint>();
        PlayerSpawnPointsFactory pointsFactory = new();

        foreach (var point in _diceSpawnPoints)
        {
            PlayerSpawnPoint spawnPoint = pointsFactory.Create(point.position);
            _playerSpawnPoints.Add(spawnPoint);
            _gameMover.AddSpawnPoint(spawnPoint);
        }
    }

    private PlayerSpawnPoint GetFreePlayerSpawnPoint() => _playerSpawnPoints.FirstOrDefault(point => point.IsContainPlayer() == false);
}
