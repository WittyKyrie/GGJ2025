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
                red.StartShowingText();
                red.onTextShowed.AddListener(() =>
                {
                    red.onTextDisappeared.AddListener(() => red.gameObject.SetActive(false));
                    red.StartDisappearingText();
                });
            }
            else
            {
                blue.gameObject.SetActive(true);
                blue.StartShowingText();
                blue.onTextShowed.AddListener(() =>
                {
                    blue.onTextDisappeared.AddListener(() => blue.gameObject.SetActive(false));
                    blue.StartDisappearingText();
                });
            }
        }
    }
}