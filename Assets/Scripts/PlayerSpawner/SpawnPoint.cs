using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public int? PlayerId { get; private set; }

    public bool IsFree { get; private set; }

    private void Awake() => IsFree = true;

    public void Occupied(int playerId)
    {
        PlayerId = playerId;
        IsFree = false;
    }

    public void Release()
    {
        PlayerId = null;
        IsFree = true;
    }
}
