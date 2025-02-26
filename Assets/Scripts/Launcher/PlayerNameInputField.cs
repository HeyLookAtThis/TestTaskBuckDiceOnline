using Photon.Pun;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_InputField))]
public class PlayerNameInputField : MonoBehaviour
{
    private const string PlayerNamePrefKey = "PlayerName";

    private TMP_InputField _inputField;

    private void Awake()
    {
        _inputField = GetComponent<TMP_InputField>();
    }

    private void Start()
    {
        string defaultName = string.Empty;

        if(_inputField != null)
        {
            if (PlayerPrefs.HasKey(PlayerNamePrefKey))
            {
                defaultName = PlayerPrefs.GetString(PlayerNamePrefKey);
                _inputField.text = defaultName;
            }
        }

        PhotonNetwork.NickName = defaultName;
    }

    public void SetPlayerName()
    {
        int maxValue = 100;

        if (string.IsNullOrEmpty(PhotonNetwork.NickName))
        {
            PhotonNetwork.NickName = "Player" + PhotonNetwork.LocalPlayer.UserId + Random.Range(0, maxValue);
            return;
        }

        PhotonNetwork.NickName = _inputField.text;
        PlayerPrefs.SetString(PlayerNamePrefKey, _inputField.text);
    }
}
