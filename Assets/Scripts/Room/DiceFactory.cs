using Photon.Pun;
using UnityEngine;

public class DiceFactory
{
    public Dice Get(Vector3 position)
    {
        GameObject gameObject = PhotonNetwork.InstantiateRoomObject(Prefabs.DicePrefab, position, Random.rotation);
        return gameObject.GetComponent<Dice>();
    }
}
