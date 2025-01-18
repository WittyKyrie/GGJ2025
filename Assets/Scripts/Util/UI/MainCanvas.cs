using DG.Tweening;
using Febucci.UI;
using Sirenix.OdinInspector;
using UnityEngine;
using Util.EventHandleSystem;

namespace Util.UI
{
    public class MainCanvas : MonoBehaviour
    {
        public TypewriterByCharacter red;
        public TypewriterByCharacter blue;

        [Button]
        public void ShowPlayerTurnText(ShowPlayerTurnText e)
        {
            if (e.IsMainPlayer)
            {
                red.gameObject.SetActive(true);
                DOVirtual.DelayedCall(1, () =>
                {
                    red.onTextDisappeared.AddListener(()=>red.gameObject.SetActive(false));
                    red.StartDisappearingText();
                });
            }
            else
            {
                blue.gameObject.SetActive(true);
                DOVirtual.DelayedCall(1, () =>
                {
                    blue.onTextDisappeared.AddListener(()=>blue.gameObject.SetActive(false));
                    blue.StartDisappearingText();
                });
            }
        }
    }
}