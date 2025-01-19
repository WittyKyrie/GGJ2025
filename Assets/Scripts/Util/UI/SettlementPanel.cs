using DG.Tweening;
using MoreMountains.Tools;
using UnityEngine;
using Util.EventHandleSystem;

namespace Util.UI
{
    public class SettlementPanel : MonoBehaviour
    {
        public GameObject content;
        public SimpleUIAnimation uIAnimation;

        public GameObject img1;
        public GameObject img2;
        public GameObject img3;
        
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
            
            img1.SetActive(true);

            DOVirtual.DelayedCall(2, () =>
            {
                img2.SetActive(true);
            });
            
            DOVirtual.DelayedCall(4, () =>
            {
                img3.SetActive(true);
                content.SetActive(true);
            });
            
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