using DG.Tweening;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class RoomNameInputPanel : MonoBehaviourPunCallbacks
{
    [SerializeField] private Launcher _launcher;
    [SerializeField] private Button _confirmButton;
    [SerializeField] private TMP_InputField _inputField;

    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public override void OnEnable()
    {
        base.OnEnable();
        _confirmButton.onClick.AddListener(TryCreateRoom);
    }

    public override void OnDisable()
    {
        base.OnDisable();
        _confirmButton.onClick.RemoveListener(TryCreateRoom);
    }

    public void Show()
    {
        gameObject.SetActive(true);

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

        gameObject.SetActive(false);
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
}
