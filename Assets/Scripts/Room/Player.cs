using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class Player : MonoBehaviourPunCallbacks
{
    private Vector3 _dicePosition;
    private Vector3 _throwPoint;

    private int _Id;
    private Dice _dice;

    public int Id => _Id;

    public void ThrowDice() => _dice.Throw(_dicePosition, _throwPoint);
    public void TakePosition(Vector3 position) => transform.position = position;

    public void Initialize(Vector3 dicePosition, Vector3 throwPosition, Dice dice, int id)
    {
        _dice = dice;
        _dicePosition = dicePosition;
        _throwPoint = throwPosition;
        _Id = id;
    }
}
