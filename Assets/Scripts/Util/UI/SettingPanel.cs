using DG.Tweening;
using MoreMountains.Tools;
using UnityEngine;

namespace Util.UI
{
    public class SettingPanel : MonoBehaviour
    {
        public SimpleUIAnimation simpleUIAnimation;

        public void ShowPanel()
        {
            gameObject.SetActive(true);
            simpleUIAnimation.DoBornAnimation();
            Time.timeScale = 0f;
        }

        public void Recover()
        {
            simpleUIAnimation.DoDieAnimation().OnComplete(() => { Time.timeScale = 1f; gameObject.SetActive(true);});
        }

        public void ChangeToHome()
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