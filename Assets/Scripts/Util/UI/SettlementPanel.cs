﻿using MoreMountains.Tools;
using UnityEngine;
using Util.EventHandleSystem;

namespace Util.UI
{
    public class SettlementPanel : MonoBehaviour
    {
        public GameObject content;
        public SimpleUIAnimation uIAnimation;
        
        private void OnEnable()
        {
            QuickEvent.SubscribeListener<SettlementEvent>(ShowSettlement);
        }

        private void OnDisable()
        {
            QuickEvent.UnsubscribeListener<SettlementEvent>(ShowSettlement);
        }

        private void ShowSettlement(SettlementEvent e)
        {
            uIAnimation.DoBornAnimation();
            content.SetActive(true);
            if (GameManager.Instance.mainPlayer.currentHealth > GameManager.Instance.subPlayer.currentHealth)
            {
                Debug.LogWarning("红方胜");
            }
            else if(GameManager.Instance.mainPlayer.currentHealth < GameManager.Instance.subPlayer.currentHealth)
            {
                Debug.LogWarning("蓝方胜");
            }
            else
            {
                Debug.LogWarning("平局胜");
            }
        }

        public void ReturnHome()
        {
            var mySettings = new MMAdditiveSceneLoadingManagerSettings
            {
                LoadingSceneName = "Loading",
                EntryFadeDuration = 0.4f,
                ExitFadeDuration = 0.4f
            };
            MMAdditiveSceneLoadingManager.LoadScene("SplashScene", mySettings);
        }
    }
}