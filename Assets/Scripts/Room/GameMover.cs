using System.Collections.Generic;
using UnityEngine;

public class GameMover : MonoBehaviour
{
    [SerializeField] private DiceRollMediator _mediator;

    private int _playersCount;
    private int _currentPlayer;
    private int _firstPlayerNumber;
    private List<PlayerSpawnPoint> _spawnPoints;

    public int CurrentPlayer => _currentPlayer;

    private void Awake()
    {
        _spawnPoints = new List<PlayerSpawnPoint>();
        _firstPlayerNumber = 0;
        _currentPlayer = _firstPlayerNumber;
    }

    public void MakePlayerMove()
    {
    }

    public void TryMakeComputerMove()
    {
        if (_spawnPoints[_currentPlayer].IsContainPlayer() == false)
            _mediator.OnRollDice();
    }

    public void ChangePlayer()
    {
        _currentPlayer++;

        if (_currentPlayer == _playersCount)
            _currentPlayer = _firstPlayerNumber;
    }

    public void AddSpawnPoint(PlayerSpawnPoint spawnPoint) => _spawnPoints.Add(spawnPoint);
}
