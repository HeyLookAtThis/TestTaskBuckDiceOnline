using Photon.Pun;
using System.Linq;
using UnityEngine;

public class PlayerFactory
{
    public Player Get(Vector3 position, Vector3 dicePlace, Vector3 throwTarget, Dice dice)
    {
        Player player = PhotonNetwork.Instantiate(Prefabs.Player, position, Quaternion.identity).GetComponent<Player>();
        player.Initialize(dicePlace, throwTarget, dice, GetId());
        return player;
    }

    private int GetId() => PhotonNetwork.PlayerList.First(player => player.IsLocal).ActorNumber;
}
