using DG.Tweening;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Dice : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private void Awake() => _rigidbody = GetComponent<Rigidbody>();

    public void Throw(Vector3 position, float force, float torque)
    {
        if (_rigidbody.IsSleeping())
        {
            _rigidbody.AddForce(Vector3.up * force, ForceMode.Impulse);
            _rigidbody.AddTorque(Random.insideUnitSphere * torque, ForceMode.Impulse);

            StartCoroutine(Mover(position));
        }
    }

    private IEnumerator Mover(Vector3 position)
    {
        while(_rigidbody.IsSleeping() == false)
            yield return null;

        if (_rigidbody.IsSleeping())
        {
            float duration = 0.1f;
            float distance = 0.2f;

            if (Vector3.Distance(position, _rigidbody.position) > distance)
                _rigidbody.DOMove(position, duration);

            yield break;
        }
    }
}
