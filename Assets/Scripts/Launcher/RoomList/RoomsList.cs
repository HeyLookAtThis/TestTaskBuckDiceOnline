using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomsList : MonoBehaviourPunCallbacks
{
    [SerializeField] private Transform _content;
    [SerializeField] private RoomButton _roomButtonPrefab;

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        for (int i = 0; i < roomList.Count; i++)
        {
            print(roomList[i].Name);
            RoomButton roomButton = Instantiate(_roomButtonPrefab, _content);
            roomButton.Initialize(roomList[i].Name);
        }
    }
}
