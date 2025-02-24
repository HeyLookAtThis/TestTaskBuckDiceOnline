using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(Dice))]
public class PhotonDiceView : MonoBehaviourPunCallbacks, IPunObservable
{
    private Dice _dice;

    public Dice Dice => _dice;

    private void Awake()
    {
        _dice = GetComponent<Dice>();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(_dice);
        }
        else
        {
            _dice = (Dice)stream.ReceiveNext();
        }
    }
}
