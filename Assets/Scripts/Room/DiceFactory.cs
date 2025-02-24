using Photon.Pun;
using UnityEngine;

public class DiceFactory
{
    private Vector3 _position;

    public DiceFactory(Vector3 posiion) => _position = posiion;

    public Dice Get()
    {
        Dice dice = Object.FindObjectOfType<Dice>();

        if (dice == null)
            return PhotonNetwork.InstantiateRoomObject(Prefabs.DicePrefab, _position, Random.rotation).GetComponent<Dice>();

        return dice;
    }
}
