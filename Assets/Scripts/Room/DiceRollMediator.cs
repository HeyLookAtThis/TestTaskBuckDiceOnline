using UnityEngine;
using UnityEngine.UI;

public class DiceRollMediator : MonoBehaviour
{
    [SerializeField] private Button _rollButton;
    [SerializeField] private Transform _diceThrowPoint;
    [SerializeField] private float _force;
    [SerializeField] private float _torque;

    private Dice _dice;

    private void OnEnable()
    {
        _rollButton.onClick.AddListener(OnRun);
    }

    private void OnDisable()
    {
        _rollButton.onClick.RemoveListener(OnRun);
    }

    public void Initialize(Dice dice) => _dice = dice;

    private void OnRun() => _dice.Throw(_diceThrowPoint.position, _force, _torque);
}
