using UnityEngine;
using UnityEngine.UI;

public class DiceRoller : MonoBehaviour
{
    [SerializeField] private Button _rollButton;
    [SerializeField] private Transform _diceThrowPoint;
    [SerializeField] private Dice _dice;
    [SerializeField] private float _force;
    [SerializeField] private float _torque;

    private void OnEnable()
    {
        _rollButton.onClick.AddListener(OnRun);
    }

    private void OnDisable()
    {
        _rollButton.onClick.RemoveListener(OnRun);
    }

    private void OnRun() => _dice.Throw(_diceThrowPoint.position, _force, _torque);
}
