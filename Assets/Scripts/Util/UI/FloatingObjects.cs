using DG.Tweening;
using UnityEngine;

namespace Util.UI
{
    public class FloatingObjects : MonoBehaviour
    {
        // 要浮动的两个物体
        public Transform object1;
        public Transform object2;

        // 浮动参数
        public float floatDuration = 2f; // 浮动一个周期的时间
        public float object1StartY = -0.7f;
        public float object1EndY = -2f;
        public float object2StartY = -2f;
        public float object2EndY = -0.7f;

        void Start()
        {
            // 确保两个物体已赋值
            if(object1 == null || object2 == null)
            {
                Debug.LogError("请在Inspector中为object1和object2赋值！");
                return;
            }

            // 为object1创建浮动动画
            object1.DOMoveY(object1EndY, floatDuration)
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo);

            // 为object2创建浮动动画
            object2.DOMoveY(object2EndY, floatDuration)
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo);
        }
    }
}