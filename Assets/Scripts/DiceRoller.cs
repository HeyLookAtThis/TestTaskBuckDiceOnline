using UnityEngine;

public class DiceRoller : MonoBehaviour
{
    [SerializeField] private Dice _dice;
    [SerializeField] private float _force;
    [SerializeField] private float _torque;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            _dice.Throw(_force, _torque);
    }
}
