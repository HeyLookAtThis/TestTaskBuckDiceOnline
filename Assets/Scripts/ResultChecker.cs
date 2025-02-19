using UnityEngine;
using UnityEngine.Events;

public class ResultChecker : MonoBehaviour
{
    private int _result;

    private UnityAction<int> _resultChanged;

    public event UnityAction<int> ResultChanged
    {
        add => _resultChanged += value;
        remove => _resultChanged -= value;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<DiceSide>(out DiceSide side))
        {
            _result = side.Value;
            _resultChanged?.Invoke(_result);
        }
    }
}
