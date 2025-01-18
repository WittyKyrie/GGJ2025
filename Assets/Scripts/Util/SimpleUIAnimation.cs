using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Util
{
    /// <summary>
    /// 提供简单的 UI 动画功能，包含“出生动画”和“销毁动画”。
    /// 可对任意挂载该脚本的 GameObject（需带 RectTransform + CanvasGroup）进行透明度、缩放、位移等处理。
    /// </summary>
    [RequireComponent(typeof(RectTransform))]
    [RequireComponent(typeof(CanvasGroup))]
    public class SimpleUIAnimation : MonoBehaviour
    {
        /// <summary>
        /// 动画配置数据模块，如动画缩放比、位移、延迟、时长、缓动类型等。
        /// </summary>
        [Serializable]
        public class BasicModule
        {
            [Tooltip("sizeDelta 缩放系数，通常用来让 UI 的 sizeDelta 按一定比例进行放大或缩小")]
            public Vector2 sizeDeltaScale = Vector2.one;

            [Tooltip("Transform 的缩放系数，会与物体原始 scale 相乘来得到目标大小")]
            public Vector2 transformScale = Vector2.one;

            [Tooltip("在动画时要移动的距离偏移量")] public Vector2 offset;

            [Tooltip("动画开始前的延迟时长")] public float delay;

            [Tooltip("动画实际播放时长")] public float duration;

            [Tooltip("缓动类型，如 Ease.OutCubic、Ease.InOutBack 等")]
            public Ease ease = Ease.OutCubic;
        }

        [Tooltip("出生动画期间是否可以交互（即是否屏蔽 Raycast），false 表示直到出生动画播放完才能交互")]
        public bool interactableWhenBorn;

        [Title("动画配置")] [Tooltip("出生动画的配置参数")] public BasicModule bornBasicModule;
        [Tooltip("销毁动画的配置参数")] public BasicModule deathBasicModule;

        [Title("必要组件")] [Tooltip("动画所操作的容器，一般是当前物体的 RectTransform")]
        public RectTransform animContainer;

        [Tooltip("CanvasGroup 用于控制透明度和 Raycast 开关")]
        public CanvasGroup canvasGroup;

        private Vector2 _initSizeDelta; // 记录 UI 初始的 sizeDelta
        private Vector3 _initScale; // 记录 UI 初始的 localScale
        private Sequence _bornSequence; // 出生动画序列
        private Sequence _dieSequence; // 销毁动画序列

        private Vector3 _initPosition; // 用于记录初始位置

        private void Awake()
        {
            if (!animContainer) animContainer = GetComponent<RectTransform>();
            if (!canvasGroup) canvasGroup = GetComponent<CanvasGroup>();

            _initSizeDelta = animContainer.sizeDelta;
            _initScale = animContainer.localScale;
            _initPosition = animContainer.localPosition; // 记录初始位置
        }


        [Button]
        public Tween DoBornAnimation()
        {
            // 重置位置
            animContainer.localPosition = _initPosition; // 确保每次开始动画时位置是原始位置

            if (_dieSequence != null && _dieSequence.IsActive()) _dieSequence.Kill(true);
            _bornSequence = DOTween.Sequence().SetUpdate(true);

            // 其他代码保持不变
            if (bornBasicModule.duration <= 0f) return _bornSequence;

            _bornSequence.AppendInterval(bornBasicModule.delay);
            canvasGroup.alpha = 0f;
            _bornSequence.Append(canvasGroup.DOFade(1f, bornBasicModule.duration));

            if (bornBasicModule.sizeDeltaScale != Vector2.one)
            {
                var oriSd = _initSizeDelta;
                animContainer.sizeDelta = Vector2.Scale(oriSd, bornBasicModule.sizeDeltaScale);
                _bornSequence.Join(
                    animContainer.DOSizeDelta(oriSd, bornBasicModule.duration).SetEase(bornBasicModule.ease)
                );
            }

            if (bornBasicModule.transformScale != Vector2.one)
            {
                var oriScale = _initScale;
                animContainer.localScale = Vector3.Scale(
                    oriScale,
                    new Vector3(bornBasicModule.transformScale.x, bornBasicModule.transformScale.y, 1f)
                );
                _bornSequence.Join(
                    animContainer.DOScale(oriScale, bornBasicModule.duration).SetEase(bornBasicModule.ease)
                );
            }

            if (bornBasicModule.offset != Vector2.zero)
            {
                var localPos = animContainer.localPosition;
                animContainer.localPosition += new Vector3(bornBasicModule.offset.x, bornBasicModule.offset.y, 0f);
                _bornSequence.Join(
                    animContainer.DOLocalMove(localPos, bornBasicModule.duration).SetEase(bornBasicModule.ease)
                );
            }

            if (!interactableWhenBorn)
            {
                canvasGroup.blocksRaycasts = false;
                _bornSequence.AppendCallback(() => canvasGroup.blocksRaycasts = true);
            }

            return _bornSequence;
        }

        [Button]
        public Tween DoDieAnimation()
        {
            // 重置位置
            animContainer.localPosition = _initPosition; // 确保每次开始动画时位置是原始位置

            if (_bornSequence != null && _bornSequence.IsActive()) _bornSequence.Kill(true);
            _dieSequence = DOTween.Sequence().SetUpdate(true);

            // 其他代码保持不变
            if (deathBasicModule.duration <= 0f) return _dieSequence;

            _dieSequence.AppendInterval(deathBasicModule.delay);
            _dieSequence.Append(canvasGroup.DOFade(0f, deathBasicModule.duration));

            if (deathBasicModule.sizeDeltaScale != Vector2.one)
            {
                var oriSd = _initSizeDelta;
                var targetSd = Vector2.Scale(oriSd, deathBasicModule.sizeDeltaScale);
                _dieSequence.Join(
                    animContainer.DOSizeDelta(targetSd, deathBasicModule.duration).SetEase(deathBasicModule.ease)
                );
            }

            if (deathBasicModule.transformScale != Vector2.one)
            {
                var oriScale = _initScale;
                var targetScale = Vector3.Scale(
                    oriScale,
                    new Vector3(deathBasicModule.transformScale.x, deathBasicModule.transformScale.y, 1f)
                );
                _dieSequence.Join(
                    animContainer.DOScale(targetScale, deathBasicModule.duration).SetEase(deathBasicModule.ease)
                );
            }

            if (deathBasicModule.offset != Vector2.zero)
            {
                var localPos = animContainer.localPosition
                               + new Vector3(deathBasicModule.offset.x, deathBasicModule.offset.y, 0f);
                _dieSequence.Join(
                    animContainer.DOLocalMove(localPos, deathBasicModule.duration).SetEase(deathBasicModule.ease)
                );
            }

            canvasGroup.blocksRaycasts = false;
            return _dieSequence;
        }


        /// <summary>
        /// 当该物体被Disable时，若动画序列还在播放，立即停止它们，避免出现潜在的内存泄露或引用问题。
        /// </summary>
        private void OnDisable()
        {
            if (_bornSequence != null && _bornSequence.IsActive()) _bornSequence.Kill(true);
            if (_dieSequence != null && _dieSequence.IsActive()) _dieSequence.Kill(true);
        }
    }
}