using System;
using Fusion;
using TMPro;
using UnityEngine;

public class PlayerController : NetworkBehaviour, IBeforeUpdate
{
    [SerializeField] private TextMeshProUGUI playerNameText;
    [SerializeField] private GameObject cam;
    [SerializeField] private float moveSpeed = 6;
    [SerializeField] private float jumpForce = 1000;

    [Networked(OnChanged = nameof(OnNicknameChanged))] private NetworkString<_8> playerName { get; set; }
    [Networked] private NetworkButtons buttonsPrev { get; set; }
    
    private float horizontal; 
    private Rigidbody2D rigid;
    
    private enum PlayerInputButtons
    {
        None,
        Jump
    }

    public override void Spawned()
    {
        rigid = GetComponent<Rigidbody2D>();

        SetLocalObjects();
    }
    
    private void SetLocalObjects()
    {
        if (Runner.LocalPlayer == Object.HasInputAuthority)
        {
            cam.SetActive(true);

            string nickName = GlobalManager.Instance.networkRunnerController.LocalPlayerNickname;
            RpcSetNickName(nickName);
        }
    }

    [Rpc(sources: RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    private void RpcSetNickName(NetworkString<_8> nickName)
    {
        playerName = nickName;
    }

    private static void OnNicknameChanged(Changed<PlayerController> changed)
    {
        changed.Behaviour.SetPlayerNickname(changed.Behaviour.playerName);
    }

    private void SetPlayerNickname(NetworkString<_8> nickName)
    {
        playerNameText.text = nickName + " " + Object.InputAuthority.PlayerId;
    }
    
    public void BeforeUpdate()
    {
        if (Runner.LocalPlayer == Object.HasInputAuthority)
        {
            const string HORIZONTAL = "Horizontal";
            horizontal = Input.GetAxisRaw(HORIZONTAL);
        }
    }
    
    
    public override void FixedUpdateNetwork()
    {
        if (Runner.TryGetInputForPlayer<PlayerData>(Object.InputAuthority, out PlayerData input))
        {
            rigid.velocity = new Vector2(input.HorizontalInput * moveSpeed, rigid.velocity.y);
            
            CheckJumpInput(input);
        }
    }

    private void CheckJumpInput(PlayerData input)
    {
        NetworkButtons pressed = input.networkButtons.GetPressed(buttonsPrev);
        if (pressed.WasPressed(buttonsPrev, PlayerInputButtons.Jump))
        {
            rigid.AddForce(Vector2.up *  jumpForce, ForceMode2D.Force);
        }

        buttonsPrev = input.networkButtons;
    }

    public PlayerData GetPlayerNetworkInput()
    {
        PlayerData data = new PlayerData();
        data.HorizontalInput = horizontal;
        data.networkButtons.Set(PlayerInputButtons.Jump, Input.GetKey(KeyCode.Space));
        return data;
    }
}