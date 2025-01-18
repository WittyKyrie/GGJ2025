using System;
using UnityEngine;
using Util;
using Util.EventHandleSystem;

public enum GameState
{
    PreAnimation,      // 开场前动画阶段
    SelectBuff,        // 抽卡牌阶段
    MainPlayerTurn,       // Player1操作阶段
    SubPlayerTurn,       // Player2操作阶段
    ReleaseItem,       // 释放道具阶段
    Drinking,          // 喝酒阶段
    Settlement,        // 结算阶段
    Pause              // 暂停阶段
}

public class GameManager : Singleton<GameManager>
{
    public Player.Player mainPlayer;
    public Player.Player subPlayer;
    
    private GameState CurrentState { get; set; }

    public delegate void OnStateChange(GameState newState);
    public event OnStateChange StateChangeEvent;

    private void Start()
    {
        ChangeState(GameState.PreAnimation);
    }

    private void Update()
    {
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
            case GameState.Pause:
                HandlePause();
                break;
            case GameState.SelectBuff:
                HandleSelectBuff();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void ChangeState(GameState newState)
    {
        CurrentState = newState;
        StateChangeEvent?.Invoke(newState);
        Debug.Log("状态已更改为: " + newState);
    }

    private void HandleSelectBuff()
    {
        ChangeState(GameState.SelectBuff);
    }
    
    private void HandlePreAnimation()
    {
        // 处理开场前动画
    }

    private void HandlePlayer1Turn()
    {
        QuickEvent.DispatchMessage(new ShowPlayerTurnText(true));
        // 处理 Player1 操作
    }

    private void HandlePlayer2Turn()
    {
        QuickEvent.DispatchMessage(new ShowPlayerTurnText(false));
        // 处理 Player2 操作
    }

    private void HandleReleaseItem()
    {
        // 处理释放道具
    }

    private void HandleDrinking()
    {
        // 处理喝酒阶段
    }

    private void HandleSettlement()
    {
        // 处理结算阶段
    }

    private void HandlePause()
    {
        // 处理暂停阶段
    }

    public void TogglePause()
    {
        if (CurrentState != GameState.Pause)
        {
            ChangeState(GameState.Pause);
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
            // 恢复到之前的状态，这里以 Player1 操作阶段为例
            ChangeState(GameState.MainPlayerTurn);
        }
    }
}
