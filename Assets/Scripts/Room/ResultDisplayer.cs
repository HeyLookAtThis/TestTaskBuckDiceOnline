using TMPro;
using UnityEngine;

public class ResultDisplayer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMeshPro;
    [SerializeField] private ResultChecker _checker;

    private void OnEnable() => _checker.ValueChanged += OnShow;
    private void OnDisable() => _checker.ValueChanged -= OnShow;

    public void OnShow(int result) => _textMeshPro.text = result.ToString();
}
