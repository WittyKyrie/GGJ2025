using System.Collections;
using DG.Tweening;
using Spine.Unity;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Util.UI
{
    public class SplashCanvas : MonoBehaviour
    {
        public GameObject talk1;
        public GameObject talk2;
        public GameObject panel;
        public SkeletonGraphic spine;
        public string spineAnimationName = "animation"; // 您的 Spine 动画名称

        private AsyncOperation asyncLoad;

        void Start()
        {
            // 初始化时确保 talk1 和 talk2 的状态
            talk1.SetActive(false);
            talk2.SetActive(false);
        }

        public void StartGame()
        {
            // 显示 talk1
            talk1.SetActive(true);

            // 使用 DOTween 延迟调用
            DOVirtual.DelayedCall(2f, () =>
            {
                talk1.SetActive(false);
                talk2.SetActive(true);
            });

            DOVirtual.DelayedCall(3f, () =>
            {
                talk2.SetActive(false);
                panel.SetActive(true);
            });
            
            DOVirtual.DelayedCall(4f, PlaySpineAnimation);

            // 开始异步加载目标场景
            StartCoroutine(LoadSceneAsync());

            // 播放点击音效
            AkSoundEngine.PostEvent(SoundEffects.Click, GameManager.Instance.gameObject);
        }

        private void PlaySpineAnimation()
        {
            if (spine == null)
            {
                Debug.LogError("Spine SkeletonGraphic is not assigned.");
                return;
            }
            spine.gameObject.SetActive(true);

            // 设置动画并订阅完成事件
            spine.AnimationState.Complete += OnSpineAnimationComplete;
            spine.AnimationState.SetAnimation(0, spineAnimationName, false);
        }

        private IEnumerator LoadSceneAsync()
        {
            // 开始异步加载目标场景
            asyncLoad = SceneManager.LoadSceneAsync("MainScene");
            // 禁止自动切换场景
            asyncLoad.allowSceneActivation = false;

            // 等待直到场景加载完成
            while (!asyncLoad.isDone)
            {
                // 当加载进度达到0.9时，表示加载完成，但还未切换场景
                if (asyncLoad.progress >= 0.9f)
                {
                    Debug.Log("Scene loaded. Waiting for animation to finish.");
                    break;
                }

                yield return null;
            }

            // 在此等待 Spine 动画完成，通过回调处理
            // 不需要在这里额外等待，因为场景切换会在动画完成的回调中处理
        }

        private void OnSpineAnimationComplete(Spine.TrackEntry trackEntry)
        {
            // 确保是目标动画完成
            if (trackEntry.Animation.Name == spineAnimationName)
            {
                // 取消订阅以防止多次调用
                spine.AnimationState.Complete -= OnSpineAnimationComplete;

                // 激活并切换到目标场景
                if (asyncLoad != null)
                {
                    asyncLoad.allowSceneActivation = true;
                }
                else
                {
                    // 如果场景尚未加载完成，等待加载完成后再激活
                    StartCoroutine(ActivateSceneWhenLoaded());
                }
            }
        }

        private IEnumerator ActivateSceneWhenLoaded()
        {
            // 等待场景加载完成
            while (asyncLoad == null || !asyncLoad.isDone)
            {
                yield return null;
            }

            // 激活并切换到目标场景
            asyncLoad.allowSceneActivation = true;
        }

        public void AboutUs()
        {
            // 实现您的关于我们逻辑
        }

        public void Exit()
        {
            Application.Quit();
        }
    }
}
