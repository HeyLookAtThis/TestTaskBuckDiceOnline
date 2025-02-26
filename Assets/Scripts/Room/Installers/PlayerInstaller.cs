using Photon.Pun;
using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private PlayerAvatar _playerPrefab;
    [SerializeField] private Transform _throwPoint;
    [SerializeField] private Transform _dicePosition;

    public override void InstallBindings()
    {
        //Container.Bind<PlayerAvatar>().FromInstance(player).AsSingle();
    }
}
