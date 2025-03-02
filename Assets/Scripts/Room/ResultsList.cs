using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultsList : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> _resultsTables;
    [SerializeField] private GameMover _gameMover;
    [SerializeField] private ResultChecker _checker;

    private void OnEnable() => _checker.ValueChanged += OnShow;
    private void OnDisable() => _checker.ValueChanged -= OnShow;

    private void OnShow(int result) => _resultsTables[_gameMover.CurrentPlayer].text = result.ToString();
}
