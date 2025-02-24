using Photon.Pun;
using UnityEngine;

public class RoomBootstrap : MonoBehaviourPunCallbacks
{
    [SerializeField] private DiceRollMediator _diceRollMediator;
    [SerializeField] private DiceSpawner _spawner;

    private void Awake()
    {
        if(PhotonNetwork.IsMasterClient)
            _spawner.Run();
    }
}
