using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateNickNamePanel : LobbyPanelBase
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Button createNicknameButton;
    [SerializeField] private int minLengthOfNickname;


    public override void InitPanel(LobbyUIManager uiManager)
    {
        base.InitPanel(uiManager);
        createNicknameButton.interactable = false;
        createNicknameButton.onClick.AddListener(OnClickCreateNickname);
        inputField.onValueChanged.AddListener(OnInputValueChanged);
    }

    private void OnInputValueChanged(string input)
    {
        createNicknameButton.interactable = input.Length >= minLengthOfNickname;
    }

    private void OnClickCreateNickname()
    {
        string nickname = inputField.text;

        if (nickname.Length > minLengthOfNickname) 
        {
            base.ClosePanel();
            lobbyUIManager.ShowPanel(LobbyPanelType.MiddleSectionPanel);
        }
    }
}
