using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateNickNamePanel : LobbyPanelBase
{
    [Header("CreateNickNamePanel: vars")]
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Button createNicknameButton;
    [SerializeField] private int minLengthOfNickname;


    private void Start()
    {
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
            // todo
        }
    }
}
