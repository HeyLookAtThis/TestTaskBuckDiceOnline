using DG.Tweening;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class JoinRoomButton : MonoBehaviourPunCallbacks
{
    [SerializeField] private Launcher _launcher;
    [SerializeField] private Button _button;

    private RoomButton _roomButton;
    private CanvasGroup _canvasGroup;


    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public override void OnEnable()
    {
        base.OnEnable();
        _button.onClick.AddListener(JoinRoom);
    }

    public override void OnDisable()
    {
        base.OnDisable();
        _button.onClick.RemoveListener(JoinRoom);
    }

    public void Show(RoomButton roomButton)
    {
        _roomButton = roomButton;

        gameObject.SetActive(true);

        float targetAlphaValue = 1.0f;
        float transparentAlphaValue = 0;
        float duration = 0.5f;

        _canvasGroup.DOFade(targetAlphaValue, duration).From(transparentAlphaValue);
    }

    public void Hide()
    {
        float transparentAlphaValue = 0;
        float duration = 0.5f;

        _canvasGroup.DOFade(transparentAlphaValue, duration);

        gameObject.SetActive(false);
    }

    private void JoinRoom() => _launcher.Connect(_roomButton.RoomName);
}
