using Cinemachine;
using ExitGames.Client.Photon;
using ExitGames.Client.Photon.StructWrapping;
using Photon.Pun;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviourPunCallbacks, ISpawnKeeper
{
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    [SerializeField] private Transform _throwPoint;
    [SerializeField] private List<Transform> _diceSpawnPoints;

    private int _spawnPointId;
    private Dice _dice;
    private Player _player;
    private List<PlayerSpawnPoint> _playerSpawnPoints;

    public Dice Dice => _dice;
    public Player Player => _player;

    //public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    //{
    //    if (PhotonNetwork.IsMasterClient)
    //    {
    //        PlayerSpawnPoint spawnPoint = GetFreePlayerSpawnPoint();
    //        _spawnPointId = spawnPoint.Id;
    //        Debug.Log($"Spawner: spawnPointId {_spawnPointId}");
    //    }
    //}

    public void Run()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            CreateDice();
            CreaterSpawnPoints();
            //SaveSpawnPointsToRoomPropities();
        }

        //PlayerSpawnPoint spawnPoint = GetFreePlayerSpawnPoint();
        //Debug.Log($"Spawner: spawnPoint {spawnPoint}");

        //CreatePlayer(spawnPoint);
    }

    private void CreateDice()
    {
        DiceFactory factory = new();
        _dice = factory.Get(_throwPoint.position);
    }

    private void CreatePlayer(PlayerSpawnPoint spawnPoint)
    {
        PlayerFactory factory = new();
        _player = factory.Get(spawnPoint.Position, spawnPoint.DicePosition);

        //spawnPoint.TryPlacePlayer(_player);
        _virtualCamera.Follow = spawnPoint.CameraTarget;
    }

    private void CreaterSpawnPoints()
    {
        _playerSpawnPoints = new List<PlayerSpawnPoint>();
        PlayerSpawnPointsFactory pointsFactory = new();

        foreach (var point in _diceSpawnPoints)
            _playerSpawnPoints.Add(pointsFactory.Create(point.position));
    }

    //private void SaveSpawnPointsToRoomPropities()
    //{
    //    foreach (var point in _playerSpawnPoints)
    //    {
    //        Hashtable roomPropities = new Hashtable
    //        {
    //            {RoomPropitiesKeys.SpawnPoint, point.Id}
    //        };

    //        PhotonNetwork.CurrentRoom.SetCustomProperties(roomPropities);
    //    }
    //}

    //private PlayerSpawnPoint GetFreePlayerSpawnPoint()
    //{

    //}

    private bool CheckPlayer(Vector3 position)
    {
        float radius = 0.5f;
        var colliders = Physics.OverlapSphere(position, radius);

       var player =  colliders.FirstOrDefault(collider => collider.TryGetComponent<Player>(out Player player));

        if (player == null)
            return false;

        return true;
    }
}
