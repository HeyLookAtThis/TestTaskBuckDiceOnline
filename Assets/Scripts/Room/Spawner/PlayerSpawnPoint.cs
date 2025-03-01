using Photon.Pun;
using UnityEngine;

public class PlayerSpawnPoint : MonoBehaviourPunCallbacks
{
    [SerializeField] private Transform _cameraTarget;

    private Vector3 _dicePosition;

    public Vector3 Position => transform.position;
    public Vector3 DicePosition => _dicePosition;
    public Transform CameraTarget => _cameraTarget;

    public void Initialize(Vector3 dicePosition)
    {
        _dicePosition = dicePosition;
    }
}
