using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class MenuManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField createLobbyInput;
    [SerializeField] private TMP_InputField joinLobbyInput;
    [SerializeField] private Button createLobbyButton;
    [SerializeField] private Button joinLobbyButton;

    private void Awake()
    {
        createLobbyButton.onClick.AddListener(CreateRoom);
        joinLobbyButton.onClick.AddListener(JoinRoom);
    }

    public void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        PhotonNetwork.CreateRoom(createLobbyInput.text, roomOptions);
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinLobbyInput.text);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("GameScene");
    }
}
