﻿using Buff;
using UnityEngine;
using Util;
using Util.EventHandleSystem;

public enum GameState
{
    SplashScene,      // 开场前动画阶段
    PreAnimation,      // 开场前动画阶段
    SelectBuff,        // 抽卡牌阶段
    MainPlayerTurn,       // Player1操作阶段
    SubPlayerTurn,       // Player2操作阶段
    ReleaseItem,       // 释放道具阶段
    Drinking,          // 喝酒阶段
    Settlement,        // 结算阶段
}

public class GameManager : Singleton<GameManager>
{
    public Player.Player mainPlayer;
    public Player.Player subPlayer;
    public BuffDataInfo buffDataInfo;
    
    private GameState CurrentState { get; set; }

    public GameState GetCurrentState() => CurrentState;
    public delegate void OnStateChange(GameState newState);
    public event OnStateChange StateChangeEvent;

    private void Start()
    {
        LoadBank("Bubble_Game");
        AkSoundEngine.PostEvent(SoundEffects.OutGameAmbBar, gameObject);
        
        ChangeState(GameState.SplashScene);
    }
    
    private void LoadBank(string bank)
    {
        var bankID = AkSoundEngine.LoadBank(bank, out _);
        if (bankID != 0)
        {
            Debug.Log($"成功加载bank: {bank}");
        }
        else
        {
            Debug.LogError($"加载bank失败: {bank}");
        }
    }

    public void ChangeState(GameState newState)
    {
        CurrentState = newState;
        StateChangeEvent?.Invoke(newState);
        Debug.Log("状态已更改为: " + newState);
        
        switch (CurrentState)
        {
            case GameState.PreAnimation:
                HandlePreAnimation();
                break;
            case GameState.MainPlayerTurn:
                HandlePlayer1Turn();
                break;
            case GameState.SubPlayerTurn:
                HandlePlayer2Turn();
                break;
            case GameState.ReleaseItem:
                HandleReleaseItem();
                break;
            case GameState.Drinking:
                HandleDrinking();
                break;
            case GameState.Settlement:
                HandleSettlement();
                break;
            case GameState.SelectBuff:
                HandleSelectBuff();
                break;
            case GameState.SplashScene:
                break;
        }
    }

    private void HandleSelectBuff()
    {
        QuickEvent.DispatchMessage(new ShowSelectBuff());
        Debug.Log("选择界面");
    }
    
    private void HandlePreAnimation()
    {
        // 处理开场前动画
        Debug.LogWarning("开场动画");
        ChangeState(GameState.SelectBuff);
    }

    private void HandlePlayer1Turn()
    {
        QuickEvent.DispatchMessage(new MainPlayerTurn());
        BindItemKeys();
        // 处理 Player1 操作
    }

    private void HandlePlayer2Turn()
    {
        QuickEvent.DispatchMessage(new SubPlayerTurn());
        BindItemKeys();
        mainPlayer.EndItemOnPour();
        // 处理 Player2 操作
    }

    private void HandleReleaseItem()
    {
        // 处理释放道具
    }

    private void HandleDrinking()
    {
        QuickEvent.DispatchMessage(new DrinkingEvent());
        UnBindItemKeys();
        subPlayer.EndItemOnPour();
        
        mainPlayer.Drinking();
        subPlayer.Drinking();
        
        mainPlayer.EndItemOnRound();
        subPlayer.EndItemOnRound();
        // 处理喝酒阶段
    }

    private void HandleSettlement()
    {
        QuickEvent.DispatchMessage(new SettlementEvent());
    }

    //启用道具按钮
    private void BindItemKeys()
    {
        UnBindItemKeys();
        InputReader.Instance.OnP1Skill1KeyInput += (state)=> OnItemKeyChanged(0,0,state);
        InputReader.Instance.OnP1Skill2KeyInput += (state)=> OnItemKeyChanged(0,1,state);
        InputReader.Instance.OnP1Skill3KeyInput += (state)=> OnItemKeyChanged(0,2,state);
        InputReader.Instance.OnP2Skill1KeyInput += (state)=> OnItemKeyChanged(1,0,state);
        InputReader.Instance.OnP2Skill2KeyInput += (state)=> OnItemKeyChanged(1,1,state);
        InputReader.Instance.OnP2Skill3KeyInput += (state)=> OnItemKeyChanged(1,2,state);
    }
    private void UnBindItemKeys()
    {
        InputReader.Instance.ClearItemKeys();
    }

    private void OnItemKeyChanged(int playerIndex, int itemIndex, InputReader.KeyState state)
    {
        if (state == InputReader.KeyState.Down)
        {
            Debug.Log($"titmest playerIndex:{playerIndex} itemIndex:{itemIndex}");
            var player = playerIndex == 0 ? Instance.mainPlayer : Instance.subPlayer;
            player.UseItem(itemIndex);
        }
    }
}
