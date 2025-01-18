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

        private void OnEnable()
        {
            QuickEvent.SubscribeListener<ShowPlayerTurnText>(ShowPlayerTurnText);
        }

        private void OnDisable()
        {
            QuickEvent.UnsubscribeListener<ShowPlayerTurnText>(ShowPlayerTurnText);
        }

        [Button]
        public void ShowPlayerTurnText(ShowPlayerTurnText e)
        {
            if (e.IsMainPlayer)
            {
                red.gameObject.SetActive(true);
                red.StartShowingText();
                DOVirtual.DelayedCall(1, () =>
                {
                    red.onTextDisappeared.AddListener(() => red.gameObject.SetActive(false));
                    GameManager.Instance.ChangeState(GameState.MainPlayerTurn);
                    red.StartDisappearingText();
                });
            }
            else
            {
                blue.gameObject.SetActive(true);
                blue.StartShowingText();
                DOVirtual.DelayedCall(1, () =>
                {
                    blue.onTextDisappeared.AddListener(() => blue.gameObject.SetActive(false));
                    GameManager.Instance.ChangeState(GameState.SubPlayerTurn);
                    blue.StartDisappearingText();
                });
            }
        }
    }
}