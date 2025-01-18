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
            AkUnitySoundEngine.PostEvent(SoundEffects.InGameBgm, GameManager.Instance.gameObject);
            AkUnitySoundEngine.PostEvent(SoundEffects.InGameAmbBar, GameManager.Instance.gameObject);
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
                    red.onTextDisappeared.AddListener(() =>
                    {
                        GameManager.Instance.ChangeState(GameState.MainPlayerTurn);
                        red.gameObject.SetActive(false);
                    });
                    red.StartDisappearingText();
                });
            }
            else
            {
                blue.gameObject.SetActive(true);
                blue.StartShowingText();
                DOVirtual.DelayedCall(1, () =>
                {
                    blue.onTextDisappeared.AddListener(() =>
                    {
                        GameManager.Instance.ChangeState(GameState.SubPlayerTurn);
                        blue.gameObject.SetActive(false);
                    });
                    blue.StartDisappearingText();
                });
            }
        }
    }
}