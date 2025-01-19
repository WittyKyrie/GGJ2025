using Buff;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Util.UI
{
    public class BuffAppear : MonoBehaviour
    {
        public TMP_Text name;
        public TMP_Text des;
        public SimpleUIAnimation SimpleUIAnimation;

        [Button]
        public void Init(string buffKey)
        {
            gameObject.SetActive(true);
            Time.timeScale = 0f;
            name.text = $"发动<shake>{BuffDataInfo.GetName(buffKey)}</shake>！！";
            des.text = BuffDataInfo.GetDescribe(buffKey);
            SimpleUIAnimation.DoBornAnimation().OnComplete(() =>
            {
                DOVirtual.DelayedCall(1f, () =>
                {
                    SimpleUIAnimation.DoDieAnimation().OnComplete(() =>
                    {
                        Time.timeScale = 1f;
                        gameObject.SetActive(false);
                    });
                });
            });
        }
    }
}