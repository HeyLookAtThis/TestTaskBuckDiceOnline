using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TMP_InputField))]
public class PlayerNameInputField : MonoBehaviour
{
    private const string PlayerNamePrefKey = "PlayerName";

    private void Start()
    {
        string defaultName = string.Empty;
        TMP_InputField inputField = GetComponent<TMP_InputField>();

        if(inputField != null)
        {
            if (PlayerPrefs.HasKey(PlayerNamePrefKey))
            {
                defaultName = PlayerPrefs.GetString(PlayerNamePrefKey);
                inputField.text = defaultName;
            }
        }

        PhotonNetwork.NickName = defaultName;
    }

    public void SetPlayerName(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            Debug.LogError("Player Name is null or empty");
            return;
        }

        PhotonNetwork.NickName = name;
        PlayerPrefs.SetString(PlayerNamePrefKey, name);
    }
}
