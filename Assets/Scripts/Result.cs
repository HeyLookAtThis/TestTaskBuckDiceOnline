using TMPro;
using UnityEngine;

public class Result : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMeshPro;
    [SerializeField] private ResultChecker _checker;

    private void OnEnable() => _checker.ResultChanged += OnShow;
    private void OnDisable() => _checker.ResultChanged -= OnShow;

    public void OnShow(int result) => _textMeshPro.text = result.ToString();
}
