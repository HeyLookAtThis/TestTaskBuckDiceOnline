using Photon.Pun;
using UnityEngine;

public class DiceFactory
{
    private Vector3 _position;

    public DiceFactory(Vector3 posiion) => _position = posiion;

    public GameObject Get()
    {
        Dice dice = Object.FindObjectOfType<PhotonDiceView>().Dice;
        Debug.Log(dice);

        if (dice == null)
            return PhotonNetwork.InstantiateRoomObject(Prefabs.DicePrefab, _position, Random.rotation);

        return dice.gameObject;
    }
}
