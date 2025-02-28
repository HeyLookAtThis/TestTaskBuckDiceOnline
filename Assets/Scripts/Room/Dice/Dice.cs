using DG.Tweening;
using Photon.Pun;
using System.Collections;
using UnityEngine;

public class Dice : MonoBehaviourPun
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _force;
    [SerializeField] private float _torque;

    private Vector3 _throwPoint;
    public void Throw(Vector3 startingThrowPosition)
    {
        this.photonView.RPC(RPCMethods.Dice.TakeThenThrow, RpcTarget.MasterClient, startingThrowPosition, _throwPoint);
    }

    public void Initialize(Vector3 throwPoint) => _throwPoint = throwPoint;

    [PunRPC]
    private void TakeThenThrow(Vector3 startingThrowPosition, Vector3 throwPosition)
    {
        StartCoroutine(MoverThenThrower(startingThrowPosition, throwPosition));
    }

    private IEnumerator MoverThenThrower(Vector3 startingThrowPosition, Vector3 throwPosition)
    {
        while (_rigidbody.IsSleeping() == false)
            yield return null;

        if (_rigidbody.IsSleeping())
        {
            float duration = 0.5f;
            _rigidbody.DOMove(startingThrowPosition, duration).OnComplete(() => AddForce(startingThrowPosition, throwPosition));
            yield break;
        }
    }

    private void AddForce(Vector3 startingThrowPosition, Vector3 throwPosition)
    {
        Vector3 direction = (Vector3.up + (throwPosition - startingThrowPosition).normalized) * _force;

        _rigidbody.AddForce(direction, ForceMode.VelocityChange);
        _rigidbody.AddTorque(Random.rotation.eulerAngles * _torque, ForceMode.VelocityChange);
    }
}
