using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class DiceRollMediator : MonoBehaviourPun
{
    [SerializeField] private Button _rollButton;

    private PlayerAvatar _player;

    private void OnEnable() => _rollButton.onClick.AddListener(OnRollDice);
    private void OnDisable() => _rollButton.onClick.RemoveListener(OnRollDice);

    private void OnRollDice() => _player.ThrowDice();

    public void Initialize(PlayerAvatar player) => _player = player;
}
