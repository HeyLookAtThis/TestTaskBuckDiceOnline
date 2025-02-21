using Cinemachine;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _camera;

    public CinemachineVirtualCamera Camera => _camera;
}
