using DG.Tweening;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using UnityEngine;

public class Dice : MonoBehaviourPun, IOnEventCallback
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _force;
    [SerializeField] private float _torque;

    private Vector3 _throwPoint;
    private Coroutine _thrower;

    private void OnEnable() => PhotonNetwork.AddCallbackTarget(this);
    private void OnDisable() => PhotonNetwork.RemoveCallbackTarget(this);

    public void OnEvent(EventData photonEvent)
    {
        byte eventCode = photonEvent.Code;

        if(eventCode == Events.RollDiceButtonClickedCode)
        {
            int positionIndex = 0;

            object[] data = (object[])photonEvent.CustomData;
            Vector3 startingThrowPosition = (Vector3)data[positionIndex];
            RunThrower(startingThrowPosition);
        }
    }

    public void RunThrower(Vector3 startingThrowPosition)
    {
        if (_thrower != null)
            StopCoroutine(_thrower);

        _thrower = StartCoroutine(MoverThenThrower(startingThrowPosition));
    }

    public void Initialize(Vector3 throwPoint) => _throwPoint = throwPoint;

    private IEnumerator MoverThenThrower(Vector3 startingThrowPosition)
    {
        while (_rigidbody.IsSleeping() == false)
            yield return null;

        if (_rigidbody.IsSleeping())
        {
            float duration = 0.5f;
            _rigidbody.DOMove(startingThrowPosition, duration).OnComplete(() => AddForce(startingThrowPosition));
            yield break;
        }
    }

    private void AddForce(Vector3 startingThrowPosition)
    {
        Vector3 direction = (Vector3.up + (_throwPoint - startingThrowPosition).normalized) * _force;

        _rigidbody.AddForce(direction, ForceMode.VelocityChange);
        _rigidbody.AddTorque(Random.rotation.eulerAngles * _torque, ForceMode.VelocityChange);
    }
}
