using Photon.Pun;
using System.Linq;
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
    public bool IsContainPlayer()
    {
        float radius = 0.5f;
        var colliders = Physics.OverlapSphere(Position, radius);

        var player = colliders.FirstOrDefault(collider => collider.TryGetComponent<Player>(out Player player));

        if (player == null)
            return false;

        return true;
    }

}
