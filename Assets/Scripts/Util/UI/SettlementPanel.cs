using DG.Tweening;
using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.UI;
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

        public Sprite redWin;
        public Sprite blueWin;
        public Sprite redBlue;

        public Image imgWin;
        
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
            
            DOVirtual.DelayedCall(6, () =>
            {
                imgWin.gameObject.SetActive(true);
                imgWin.rectTransform.DOScale(3, 1f).SetEase(Ease.OutBack);
                
                if (GameManager.Instance.mainPlayer.currentHealth > GameManager.Instance.subPlayer.currentHealth)
                {
                    imgWin.sprite = redWin;
                }
                else if(GameManager.Instance.mainPlayer.currentHealth < GameManager.Instance.subPlayer.currentHealth)
                {
                    imgWin.sprite = blueWin;
                }
                else
                {
                    imgWin.sprite = redBlue;
                }
            });
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