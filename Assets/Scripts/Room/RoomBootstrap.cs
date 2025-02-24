using UnityEngine;

public class RoomBootstrap : MonoBehaviour
{
    [SerializeField] private Transform _diceSpawnPoint;
    [SerializeField] private DiceRollMediator _diceRollMediator;

    private void Start()
    {
        DiceFactory diceFactory = new(_diceSpawnPoint.position);
        Dice dice = diceFactory.Get();
        _diceRollMediator.Initialize(dice);
    }
}
