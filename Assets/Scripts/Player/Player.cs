using Fusion;
using UnityEngine;

public class Player : NetworkBehaviour
{
    [SerializeField] private Transform _cameraTarget;

    public Transform CameraTarget => _cameraTarget;
}
