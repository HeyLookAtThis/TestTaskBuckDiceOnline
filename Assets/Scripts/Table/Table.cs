using UnityEngine;

public class Table : MonoBehaviour
{
    [SerializeField] private Transform _cameraTarget;

    public Transform CameraTarget => _cameraTarget;
}
