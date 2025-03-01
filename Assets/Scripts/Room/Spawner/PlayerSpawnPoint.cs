using Photon.Pun;
using UnityEngine;

public class PlayerSpawnPoint : MonoBehaviourPunCallbacks
{
    [SerializeField] private Transform _cameraTarget;

    private Player _player;
    private Vector3 _dicePosition;

    public bool IsEmpty {  get; private set; }
    public Vector3 Position => transform.position;
    public Vector3 DicePosition => _dicePosition;
    public Transform CameraTarget => _cameraTarget;
    public int Id => this.photonView.ViewID;

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        if (otherPlayer.ActorNumber == _player.Id)
            this.photonView.RPC(RPCMethods.PlayerSpawnPoint.RemovePlayer, RpcTarget.MasterClient, otherPlayer);
    }
    public void Initialize(Vector3 dicePosition)
    {
        IsEmpty = true;
        _dicePosition = dicePosition;
    }

    public bool TryPlacePlayer(Player player)
    {
        if (IsEmpty)
        {
            this.photonView.RPC(RPCMethods.PlayerSpawnPoint.PlacePlayer, RpcTarget.MasterClient, player);
            return true;
        }

        return false;
    }

    [PunRPC]
    private void RemovePlayer(Photon.Realtime.Player player)
    {
        if(player.ActorNumber == _player.Id)
        {
            _player = null;
            IsEmpty = true;
        }
    }

    [PunRPC]
    private void PlacePlayer(Player player)
    {
        _player = player;
        IsEmpty = false;
    }
}
