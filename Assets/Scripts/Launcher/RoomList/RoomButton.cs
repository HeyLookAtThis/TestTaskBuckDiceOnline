using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class RoomButton : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [SerializeField] private TextMeshProUGUI _roomName;

    private JoinRoomButton _joinRoomButton;

    public string RoomName { get; private set; }

    public void OnSelect(BaseEventData eventData) => _joinRoomButton.Show(this);
    public void OnDeselect(BaseEventData eventData) => _joinRoomButton.Hide();

    public void Initialize(string name)
    {
        _roomName.text = name;
        RoomName = name;
    }
}
