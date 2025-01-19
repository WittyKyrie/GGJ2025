using UnityEngine;
using DG.Tweening;

namespace Util.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class Shake : MonoBehaviour
    {
        [Header("Shake Settings")]
        [Tooltip("震动的强度")]
        public float strength = 10f;

        [Tooltip("震动的振动次数")]
        public int vibrato = 10;

        [Tooltip("震动的随机性")]
        public float randomness = 90f;

        [Tooltip("震动的持续时间（秒）")]
        public float duration = 1f;

        [Tooltip("是否无限循环震动")]
        public bool infinite = true;

        private RectTransform rectTransform;
        private Sequence shakeSequence;

        void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        void OnEnable()
        {
            StartShaking();
        }

        void OnDisable()
        {
            StopShaking();
        }

        /// <summary>
        /// 开始震动
        /// </summary>
        private void StartShaking()
        {
            if (shakeSequence != null && shakeSequence.IsActive())
            {
                shakeSequence.Kill();
            }

            shakeSequence = DOTween.Sequence();

            if (infinite)
            {
                shakeSequence.Append(rectTransform.DOShakePosition(duration, strength, vibrato, randomness)
                    .SetEase(Ease.Linear))
                            .SetLoops(-1, LoopType.Restart);
            }
            else
            {
                shakeSequence.Append(rectTransform.DOShakePosition(duration, strength, vibrato, randomness)
                    .SetEase(Ease.Linear));
            }

            shakeSequence.Play();
        }

        /// <summary>
        /// 停止震动
        /// </summary>
        private void StopShaking()
        {
            if (shakeSequence != null)
            {
                shakeSequence.Kill();
                shakeSequence = null;
                // 重置位置
                rectTransform.localPosition = Vector3.zero;
            }
        }
    }
}
