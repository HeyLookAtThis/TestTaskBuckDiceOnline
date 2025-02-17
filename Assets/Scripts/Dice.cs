using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Dice : MonoBehaviour
{
    private Rigidbody _rigidbody;

    public Transform Transform => transform;

    private void Awake() => _rigidbody = GetComponent<Rigidbody>();

    public void Throw(float force, float torque)
    {
        _rigidbody.AddForce(Vector3.up * force);
        _rigidbody.AddTorque(Random.insideUnitSphere * torque);
    }
}
