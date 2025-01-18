using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            Time.timeScale = 1f;
            SceneManager.LoadScene("SplashScene");
        }
    }
}