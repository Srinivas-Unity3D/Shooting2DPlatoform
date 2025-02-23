using System;
using Fusion;
using UnityEngine;

public class PlayerController : NetworkBehaviour, IBeforeUpdate
{
    [SerializeField] private float moveSpeed = 6;

    private float horizontal; 
    private Rigidbody2D rigid;

    public override void Spawned()
    {
        rigid = GetComponent<Rigidbody2D>();
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
        }
    }

    public PlayerData GetPlayerNetworkInput()
    {
        PlayerData data = new PlayerData();
        data.HorizontalInput = horizontal;
        return data;
    }
}