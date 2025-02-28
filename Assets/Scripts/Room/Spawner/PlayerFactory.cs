using Photon.Pun;
using System.Linq;
using UnityEngine;

public class PlayerFactory
{
    public Player Get(Vector3 position, Vector3 dicePlace)
    {
        Player player = PhotonNetwork.Instantiate(Prefabs.Player, position, Quaternion.identity).GetComponent<Player>();
        player.Initialize(dicePlace, GetId());
        return player;
    }

    private int GetId() => PhotonNetwork.PlayerList.First(player => player.IsLocal).ActorNumber;
}
