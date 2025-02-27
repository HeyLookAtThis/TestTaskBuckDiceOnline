using Photon.Pun;
using UnityEngine;

public class RoomBootstrap : MonoBehaviourPunCallbacks
{
    [SerializeField] private DiceRollMediator _mediator;
    [SerializeField] private Spawner _spawner;

    private void Awake()
    {
        _spawner.Run();
        _mediator.Initialize(_spawner);
    }
}
