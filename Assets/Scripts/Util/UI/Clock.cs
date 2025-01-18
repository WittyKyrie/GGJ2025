﻿using System;
using UnityEngine;
using TMPro;
using System.Collections;
using Util.EventHandleSystem;

namespace Util.UI
{
    public class Clock : MonoBehaviour
    {
        public GameObject img;
        [SerializeField] private int countdownTime = 10; // 倒计时的秒数
        [SerializeField] private TMP_Text countdownText; // TextMeshPro组件，用于显示倒计时

        private Coroutine _countdownCoroutine;
        private Action _onCountdownEnd;

        private void OnEnable()
        {
            QuickEvent.SubscribeListener<MainPlayerTurn>(MainPlayerTurn);
            QuickEvent.SubscribeListener<SubPlayerTurn>(SubPlayerTurn);
            QuickEvent.SubscribeListener<BeerIsFullEvent>(StopCountdown);
        }

        private void OnDisable()
        {
            QuickEvent.UnsubscribeListener<MainPlayerTurn>(MainPlayerTurn);
            QuickEvent.UnsubscribeListener<SubPlayerTurn>(SubPlayerTurn);
            QuickEvent.UnsubscribeListener<BeerIsFullEvent>(StopCountdown);
        }

        private void MainPlayerTurn(MainPlayerTurn e)
        {
            StartCountdown(() => QuickEvent.DispatchMessage(new ShowPlayerTurnText(false)));
        }

        private void SubPlayerTurn(SubPlayerTurn e)
        {
            //todo:可能存在满了也停止的原因
            StartCountdown(() => GameManager.Instance.ChangeState(GameState.Drinking));
        }

        // 开始倒计时的方法
        private void StartCountdown(Action onCountdownEnd)
        {
            if (_countdownCoroutine != null)
            {
                StopCoroutine(_countdownCoroutine); // 如果之前有倒计时，先停止
            }

            countdownTime = 10;
            img.SetActive(true);
            _onCountdownEnd = onCountdownEnd;
            _countdownCoroutine = StartCoroutine(CountdownRoutine(onCountdownEnd));
        }

        // 倒计时协程
        private IEnumerator CountdownRoutine(Action onCountdownEnd)
        {
            while (countdownTime > 0)
            {
                if (countdownText != null)
                {
                    countdownText.text = countdownTime.ToString(); // 更新TextMeshPro组件的文本
                }

                Debug.Log($"Time left: {countdownTime} seconds");
                countdownTime--;
                yield return new WaitForSeconds(1f); // 每秒减少
            }

            Debug.Log("Countdown finished!");

            if (countdownText != null)
            {
                countdownText.text = "0"; // 倒计时结束时显示0
            }

            // 倒计时结束后执行回调方法
            img.SetActive(false);
            onCountdownEnd?.Invoke();
        }

        // 立即结束倒计时的方法
        public void StopCountdown(BeerIsFullEvent e)
        {
            if (_countdownCoroutine != null)
            {
                StopCoroutine(_countdownCoroutine); // 停止当前协程
                _countdownCoroutine = null;
            }

            countdownTime = 0; // 将倒计时设置为0

            if (countdownText != null)
            {
                countdownText.text = "0"; // 更新显示
            }

            img.SetActive(false); // 隐藏倒计时UI
            _onCountdownEnd?.Invoke(); // 调用回调
        }
    }
}