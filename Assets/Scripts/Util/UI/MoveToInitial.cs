using UnityEngine;
using DG.Tweening;

namespace Util.UI
{
    [RequireComponent(typeof(Transform))]
    public class MoveToInitial : MonoBehaviour
    {
        [Header("Move Settings")]
        [Tooltip("对象在启用时要移动到的自定义位置")]
        public Vector3 customPosition = Vector3.zero;

        [Tooltip("移动回初始位置的持续时间（秒）")]
        public float moveDuration = 1f;

        [Tooltip("移动的缓动类型")]
        public Ease moveEase = Ease.OutBack;

        private Vector3 initialPosition;
        private Tween moveTween;

        void Awake()
        {
            // 记录对象的初始位置
            initialPosition = transform.position;
        }

        void OnEnable()
        {
            // 设置对象的位置为自定义位置
            transform.position = customPosition;

            // 使用 DOTween 移动到初始位置
            moveTween = transform.DOMove(initialPosition, moveDuration)
                .SetEase(moveEase)
                .OnComplete(OnMoveComplete);
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
        /// Tween 完成时的回调
        /// </summary>
        private void OnMoveComplete()
        {
            // 这里可以添加移动完成后的逻辑，例如触发事件
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
            }
        }
    }
}