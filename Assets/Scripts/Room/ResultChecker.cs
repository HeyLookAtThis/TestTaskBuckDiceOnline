using UnityEngine;
using UnityEngine.Events;

public class ResultChecker : MonoBehaviour
{
    private int _value;

    private UnityAction<int> _valueChanged;

    public event UnityAction<int> ValueChanged
    {
        add => _valueChanged += value;
        remove => _valueChanged -= value;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<DiceSide>(out DiceSide side))
        {
            _value = side.Value;
            _valueChanged?.Invoke(_value);
        }
    }
}
