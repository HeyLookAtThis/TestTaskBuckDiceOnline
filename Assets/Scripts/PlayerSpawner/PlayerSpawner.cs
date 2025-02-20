using Cinemachine;
using Fusion;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : SimulationBehaviour, IPlayerJoined, IPlayerLeft
{
    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private List<SpawnPoint> _points;

    public void PlayerJoined(PlayerRef player)
    {
        if (player == Runner.LocalPlayer)
        {
            var newPlayer = Runner.Spawn(_playerPrefab, GetFreePoint(player.PlayerId), Quaternion.identity);

            if (newPlayer.TryGetComponent<Player>(out Player component))
                _camera.Follow = component.CameraTarget;
        }
    }

    public void PlayerLeft(PlayerRef player)
    {
        if (player == Runner.LocalPlayer)
            foreach (var point in _points)
                if (point.PlayerId == player.PlayerId)
                    point.Release();
    }

    private Vector3 GetFreePoint(int playerId)
    {
        Vector3 coordinate = new Vector3();

        foreach (var point in _points)
        {
            if (point.IsFree)
            {
                coordinate = point.transform.position;
                point.Occupied(playerId);
                return coordinate;
            }
        }

        return coordinate;
    }
}
