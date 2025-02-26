using ExitGames.Client.Photon;
using ExitGames.Client.Photon.StructWrapping;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PlayerAvatar : MonoBehaviourPunCallbacks
{
    private Vector3 _dicePosition;
    private Vector3 _throwPoint;

    private Dice _dice;

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if(targetPlayer.IsLocal)
        {
            changedProps.TryGetValue(Prefabs.DicePrefab, out Dice dice);
            _dice = dice;
        }
    }

    public void ThrowDice() => _dice.Throw(_throwPoint, _dicePosition);

    public void TakePosition(Vector3 position) => transform.position = position;

    public void Initialize(Vector3 dicePosition, Vector3 throwPosition, Dice dice)
    {
        _dice = dice;
        _dicePosition = dicePosition;
        _throwPoint = throwPosition;
    }
}
