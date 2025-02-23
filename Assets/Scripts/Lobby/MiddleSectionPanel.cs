using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MiddleSectionPanel : LobbyPanelBase
{
    [Header("MiddleSectionPanel Vars")]
    [SerializeField] private Button joinRandomRoomBtn;
    [SerializeField] private Button joinRoomByArgBtn;
    [SerializeField] private Button createRoomBtn;

    [SerializeField] private TMP_InputField joinRoomByArgsInputField;
    [SerializeField] private TMP_InputField createRoomInputField;

    [SerializeField] private int maxLengthOfField = 2;

    private NetworkRunnerController networkRunnerController;

    public override void InitPanel(LobbyUIManager uiManager)
    {
        base.InitPanel(uiManager);
        networkRunnerController = GlobalManager.Instance.networkRunnerController;

        joinRandomRoomBtn.onClick.AddListener(JoinRandoRoom);
        joinRoomByArgBtn.onClick.AddListener(() => CreateRoom(GameMode.Client, joinRoomByArgsInputField.text));
        createRoomBtn.onClick.AddListener(() => CreateRoom(GameMode.Host, createRoomInputField.text));
    }

    private void CreateRoom(GameMode mode, string field) 
    {
        if (field.Length >= maxLengthOfField) 
        {
            Debug.Log($"------------{mode}------------");
            networkRunnerController.StartGame(mode, field);
        }
    }

    private void JoinRandoRoom()
    {
        Debug.Log($"------------JoinRandomRoom!------------");
        networkRunnerController.StartGame(GameMode.AutoHostOrClient, string.Empty);
    }
}
