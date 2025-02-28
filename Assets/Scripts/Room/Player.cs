using Photon.Pun;
using UnityEngine;

public class Player : MonoBehaviourPunCallbacks
{
    private Vector3 _dicePosition;
    private int _id;
    public Vector3 DicePosition => _dicePosition;
    public int Id => _id;
    public void Initialize(Vector3 dicePosition, int id)
    {
        _dicePosition = dicePosition;
        _id = id;
    }
}
