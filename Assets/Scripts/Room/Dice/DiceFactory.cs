using Photon.Pun;
using UnityEngine;

public class DiceFactory
{
    public Dice Get(Vector3 position)
    {
        return PhotonNetwork.InstantiateRoomObject(Prefabs.DicePrefab, position, Random.rotation).GetComponent<Dice>();
    }
}
