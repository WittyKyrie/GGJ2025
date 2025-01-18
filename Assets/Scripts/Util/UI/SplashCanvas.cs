using MoreMountains.Tools;
using UnityEngine;

namespace Util.UI
{
    public class SplashCanvas : MonoBehaviour
    {
        public void StartGame()
        {
            var mySettings = new MMAdditiveSceneLoadingManagerSettings
            {
                LoadingSceneName = "Loading",
                EntryFadeDuration = 0.4f,
                ExitFadeDuration = 0.4f
            };
            MMAdditiveSceneLoadingManager.LoadScene("MainScene", mySettings);
            AkUnitySoundEngine.PostEvent(SoundEffects.Click, GameManager.Instance.gameObject);
        }

        public void AboutUs()
        {
        
        }

        public void Exit()
        {
            Application.Quit();
        }
    }
}
