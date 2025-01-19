using UnityEngine;
using DG.Tweening;

namespace Util.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class MoveToInitialUI : MonoBehaviour
    {
        [Header("Move Settings")]
        [Tooltip("对象在启用时要移动到的自定义位置 (相对于父级的锚点位置)")]
        public Vector2 customPosition = Vector2.zero;

        [Tooltip("移动回初始位置的持续时间（秒）")]
        public float moveDuration = 1f;

        [Tooltip("移动的缓动类型")]
        public Ease moveEase = Ease.OutBack;

        [Tooltip("是否在移动完成后回调")]
        public bool invokeOnComplete = false;

        [Tooltip("移动完成后的回调方法名（如果启用）")]
        public string onCompleteMethodName = "OnMoveComplete";

        private RectTransform rectTransform;
        private Vector2 initialAnchoredPosition;
        private Tween moveTween;

        void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            // 记录对象的初始锚点位置
            initialAnchoredPosition = rectTransform.anchoredPosition;
        }

        void OnEnable()
        {
            // 设置对象的位置为自定义位置
            rectTransform.anchoredPosition = customPosition;

            // 使用 DOTween 移动到初始位置
            moveTween = rectTransform.DOAnchorPos(initialAnchoredPosition, moveDuration)
                                     .SetEase(moveEase)
                                     .OnComplete(OnMoveCompleteHandler);
        }

        void OnDisable()
        {
            // 如果 Tween 仍在运行，停止它
            if (moveTween != null && moveTween.IsActive())
            {
                moveTween.Kill();
            }
        }

        /// <summary>
        /// Tween 完成时的回调处理
        /// </summary>
        private void OnMoveCompleteHandler()
        {
            if (invokeOnComplete && !string.IsNullOrEmpty(onCompleteMethodName))
            {
                // 通过反射调用回调方法
                Invoke(onCompleteMethodName, 0f);
            }

            Debug.Log($"{gameObject.name} has moved to its initial position.");
        }

        /// <summary>
        /// 外部调用，强制停止移动
        /// </summary>
        public void StopMove()
        {
            if (moveTween != null && moveTween.IsActive())
            {
                moveTween.Kill();
                // 可选：重置位置为初始位置
                rectTransform.anchoredPosition = initialAnchoredPosition;
            }
        }

        /// <summary>
        /// 外部调用，开始移动
        /// </summary>
        public void BeginMove()
        {
            // 如果当前 Tween 正在运行，先停止它
            if (moveTween != null && moveTween.IsActive())
            {
                moveTween.Kill();
            }

            // 设置对象的位置为自定义位置
            rectTransform.anchoredPosition = customPosition;

            // 使用 DOTween 移动到初始位置
            moveTween = rectTransform.DOAnchorPos(initialAnchoredPosition, moveDuration)
                                     .SetEase(moveEase)
                                     .OnComplete(OnMoveCompleteHandler);
        }
    }
}
