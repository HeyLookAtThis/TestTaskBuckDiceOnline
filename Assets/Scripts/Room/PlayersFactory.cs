using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersFactory
{
    public PlayerAvatar Get(Vector3 position, Transform tableTransform)
    {
        GameObject gameObject = PhotonNetwork.InstantiateRoomObject(Prefabs.PlayerAvatar, position, Quaternion.identity);
        gameObject.transform.forward = -tableTransform.forward;
        return gameObject.GetComponent<PlayerAvatar>();
    }
}
