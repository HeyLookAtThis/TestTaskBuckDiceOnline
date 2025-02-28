using Photon.Pun;
using UnityEngine;

public class DiceFactory
{
    public Dice Get(Vector3 position)
    {
        Dice dice = PhotonNetwork.InstantiateRoomObject(Prefabs.DicePrefab, position, Random.rotation).GetComponent<Dice>();
        dice.Initialize(position);
        return dice;
    }
}
