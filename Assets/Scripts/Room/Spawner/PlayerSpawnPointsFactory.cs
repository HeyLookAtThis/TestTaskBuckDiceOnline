using Photon.Pun;
using UnityEngine;

public class PlayerSpawnPointsFactory
{
    public PlayerSpawnPoint Create(Vector3 dicePosition)
    {
        PlayerSpawnPoint spawnPoint = PhotonNetwork.InstantiateRoomObject(Prefabs.PlayerSpawnPoint, GetPosition(dicePosition), Quaternion.identity).GetComponent<PlayerSpawnPoint>();
        spawnPoint.Initialize(dicePosition);
        return spawnPoint;
    }

    private Vector3 GetPosition(Vector3 dicePosition)
    {
        float indent = 2;
        Vector3 newPosition = dicePosition;
        newPosition.y = 0;
        return newPosition * indent;
    }
}
