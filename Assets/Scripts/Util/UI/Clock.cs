using System;
using UnityEngine;
using TMPro;
using System.Collections;
using Util.EventHandleSystem;

namespace Util.UI
{
    public class Clock : MonoBehaviour
    {
        public GameObject img;
        [SerializeField] private int countdownTime = 5; // 倒计时的秒数
        [SerializeField] private TMP_Text countdownText; // TextMeshPro组件，用于显示倒计时

        private Coroutine _countdownCoroutine;

        private void OnEnable()
        {
            QuickEvent.SubscribeListener<MainPlayerTurn>(MainPlayerTurn);
            QuickEvent.SubscribeListener<SubPlayerTurn>(SubPlayerTurn);
        }

        private void OnDisable()
        {
            QuickEvent.UnsubscribeListener<MainPlayerTurn>(MainPlayerTurn);
            QuickEvent.UnsubscribeListener<SubPlayerTurn>(SubPlayerTurn);
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
        public void StartCountdown(Action onCountdownEnd)
        {
            if (_countdownCoroutine != null)
            {
                StopCoroutine(_countdownCoroutine); // 如果之前有倒计时，先停止
            }

            countdownTime = 5;
            img.SetActive(true);
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
    }
}