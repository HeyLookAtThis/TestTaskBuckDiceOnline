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
        int pointId = 0;

        if (PhotonNetwork.IsMasterClient)
        {
            PlayerSpawnPoint spawnPoint = GetFreePlayerSpawnPoint();
            pointId = spawnPoint.Id;

            spawnPoint.TryPlacePlayer(newPlayer);
        }

        if (newPlayer.IsLocal)
        {
            PlayerSpawnPoint spawnPoint = PhotonNetwork.GetPhotonView(pointId).GetComponent<PlayerSpawnPoint>();
            CreatePlayer(spawnPoint);
            _virtualCamera.Follow = spawnPoint.CameraTarget;
        }
    }
    public void Run()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            CreateDice();
            CreatePlayerSpawnPoints();
        }
    }
    private void CreateDice()
    {
        DiceFactory factory = new();
        _dice = factory.Get(_diceSpawnPoints[0].position);
    }
    private void CreatePlayer(PlayerSpawnPoint spawnPoint)
    {
        PlayerFactory factory = new();
        _player = factory.Get(spawnPoint.Position, spawnPoint.DicePosition);
    }
    private void CreatePlayerSpawnPoints()
    {
        _playerSpawnPoints = new List<PlayerSpawnPoint>();
        PlayerSpawnPointsFactory pointsFactory = new();

        foreach (var point in _diceSpawnPoints)
            _playerSpawnPoints.Add(pointsFactory.Create(point.position));
    }
    private PlayerSpawnPoint GetFreePlayerSpawnPoint() => _playerSpawnPoints.First(point => point.IsEmpty);
}
