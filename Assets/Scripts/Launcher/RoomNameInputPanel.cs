using DG.Tweening;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class RoomNameInputPanel : MonoBehaviourPunCallbacks
{
    private const string CreateCommand = "Create";
    private const string FindCommand = "Find";

    [SerializeField] private Launcher _launcher;
    [SerializeField] private Button _confirmButton;
    [SerializeField] private TMP_InputField _inputField;

    private CanvasGroup _canvasGroup;
    private string _currentCommand;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Show(string command)
    {
        gameObject.SetActive(true);
        _currentCommand = command;

        AddActionCallback();

        float targetAlphaValue = 1.0f;
        float transparentAlphaValue = 0;
        float duration = 0.5f;

        _canvasGroup.DOFade(targetAlphaValue, duration).From(transparentAlphaValue);
    }

    private void Hide()
    {
        float transparentAlphaValue = 0;
        float duration = 0.5f;

        _canvasGroup.DOFade(transparentAlphaValue, duration);

        RemoveActionCallback();
        gameObject.SetActive(false);
    }

    private void AddActionCallback()
    {
        switch (_currentCommand)
        {
            case CreateCommand:
                _confirmButton.onClick.AddListener(TryCreateRoom);
                break;

            case FindCommand:
                _confirmButton.onClick.AddListener(TryFindRoom);
                break;
        }
    }

    private void RemoveActionCallback()
    {
        switch (_currentCommand)
        {
            case CreateCommand:
                _confirmButton.onClick.RemoveListener(TryCreateRoom);
                break;

            case FindCommand:
                _confirmButton.onClick.RemoveListener(TryFindRoom);
                break;
        }
    }

    private void TryCreateRoom()
    {
        string roomName = _inputField.text;

        if (string.IsNullOrEmpty(roomName) == false)
        {
            _launcher.Create(roomName);
            Hide();
        }
    }

    private void TryFindRoom()
    {
        string roomName = _inputField.text;

        if (string.IsNullOrEmpty(roomName) == false)
        {
            _launcher.FindGame(roomName);
            Hide();
        }
    }
}
